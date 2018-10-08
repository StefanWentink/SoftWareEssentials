namespace SWE.EventSourcing.Models
{
    using SWE.BasicType.Utilities;
    using SWE.EventSourcing.Interfaces;
    using SWE.Expression.Extensions;
    using System;
    using System.Linq.Expressions;

    public class PropertyMutation<T, TValue>
        : IPropertyAction<T>
    {
        private readonly Action<T> _revertValueAction;

        private readonly Action<T> _valueAction;

        public TValue Value { get; }

        /// <summary>
        /// Expression setting a properties value. Must be at least internally visible.
        /// </summary>
        public Expression<Func<T, TValue>> PropertySelector { get; }

        [Obsolete("Only for serialisation.", true)]
        public PropertyMutation()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="PropertyMutation"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertySelector"></param>
        /// <exception cref="ArgumentException">Throws when <see cref="TValue"/> not invertable.</exception>
        public PropertyMutation(
            TValue value,
            Expression<Func<T, TValue>> propertySelector)
        {
            PropertySelector = propertySelector;
            Value = value;

            if (!CalculateUtilities.TryInvert(value, out var _invertValue))
            {
                throw new ArgumentException($"{nameof(value)} is not invertable.", nameof(value));
            }

            _valueAction = propertySelector.ToAddAction(value);

            _revertValueAction = propertySelector.ToAddAction(_invertValue);
        }

        public Action<T> GetRevertValueAction()
        {
            return _revertValueAction;
        }

        public Action<T> GetApplyValueAction()
        {
            return _valueAction;
        }
    }
}