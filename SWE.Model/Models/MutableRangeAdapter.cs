namespace SWE.Model.Models
{
    using SWE.Model.Interfaces;
    using SWE.Reflection.Extensions;
    using System;
    using System.Linq.Expressions;

    public class MutableRangeAdapter<T, TValue> : RangeAdapter<T, TValue>, IRangeWith<MutableRangeAdapter<T, TValue>, TValue>
        where T : IWith<T>
        where TValue : IComparable<TValue>
    {
        private readonly Expression<Func<T, TValue>> _fromExpression;

        private readonly Expression<Func<T, TValue>> _tillExpression;

        private readonly Func<T, TValue> _fromSelector;

        private readonly Func<T, TValue> _tillSelector;

        private readonly Action<T, TValue> _fromSetter;

        private readonly Action<T, TValue> _tillSetter;

        /// <inheritdoc />
        public override TValue From
        {
            get => _fromSelector(_value);
            protected set => _fromSetter(_value, value);
        }

        public void SetFrom(TValue from)
        {
            From = from;
        }

        /// <inheritdoc />
        public override TValue Till
        {
            get => _tillSelector(_value);
            protected set => _tillSetter(_value, value);
        }

        public void SetTill(TValue till)
        {
            Till = till;
        }

        [Obsolete("Only for serialization.", true)]
        public MutableRangeAdapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromExpression"></param>
        /// <param name="tillExpression"></param>
        public MutableRangeAdapter(
            T value,
            Expression<Func<T, TValue>> fromExpression,
            Expression<Func<T, TValue>> tillExpression)
            : base(value)
        {
            _fromExpression = fromExpression;
            _tillExpression = tillExpression;
            _fromSetter = fromExpression.SetValueExpression().Compile();
            _tillSetter = tillExpression.SetValueExpression().Compile();
            _fromSelector = fromExpression.Compile();
            _tillSelector = tillExpression.Compile();
        }

        /// <inheritdoc />
        new public MutableRangeAdapter<T, TValue> With(TValue from, TValue till)
        {
            return new MutableRangeAdapter<T, TValue>(_value.With(), _fromExpression, _tillExpression)
            {
                From = from,
                Till = till
            };
        }
    }
}