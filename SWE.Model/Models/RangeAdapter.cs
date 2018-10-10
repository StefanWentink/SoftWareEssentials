namespace SWE.Model.Models
{
    using SWE.Model.Interfaces;
    using System;

    public class RangeAdapter<T, TValue> : IRangeWith<RangeAdapter<T, TValue>, TValue>
        where T : IWith<T>
        where TValue : IComparable<TValue>
    {
        protected readonly T _value;

        /// <inheritdoc />
        public virtual TValue From { get; protected set; }

        /// <inheritdoc />
        public virtual TValue Till { get; protected set; }

        [Obsolete("Only for serialization.", true)]
        public RangeAdapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        protected RangeAdapter(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromSelector"></param>
        /// <param name="tillSelector"></param>
        public RangeAdapter(T value, Func<T, TValue> fromSelector, Func<T, TValue> tillSelector)
            : this(value, fromSelector(value), tillSelector(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fromSelector"></param>
        /// <param name="till"></param>
        public RangeAdapter(T value, Func<T, TValue> fromSelector, TValue till)
            : this(value, fromSelector(value), till)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="tillSelector"></param>
        public RangeAdapter(T value, TValue from, Func<T, TValue> tillSelector)
            : this(value, from, tillSelector(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAdapter"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="till"></param>
        public RangeAdapter(T value, TValue from, TValue till)
        {
            _value = value;
            From = from;
            Till = till;
        }

        /// <summary>
        /// Get <see cref="T"/>.
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return _value;
        }

        /// <inheritdoc />
        public RangeAdapter<T, TValue> With(TValue from, TValue till)
        {
            return new RangeAdapter<T, TValue>(_value.With(), from, till);
        }
    }
}