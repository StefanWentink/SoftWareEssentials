namespace SWE.EventSourcing.Models
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Expression.Extensions;
    using SWE.Reflection.Extensions;
    using System;
    using System.Linq.Expressions;

    public class PropertyChange<T, TValue>
        : IPropertyChange<TValue>
        , IPropertyAction<T>
    {
        private readonly Expression<Func<T, TValue>> _propertySelector;

        /// <summary>
        /// Expression setting a properties value. Must be at least internally visible.
        /// </summary>
        public Expression<Func<T, TValue>> PropertySelector { get; }

        public TValue PreviousValue { get; set; }

        public TValue Value { get; set; }

        [Obsolete("Only for serialisation.", true)]
        public PropertyChange()
        {
        }

        public PropertyChange(
            TValue previousValue,
            TValue value,
            Expression<Func<T, TValue>> propertySelector)
        {
            PreviousValue = previousValue;
            Value = value;
            _propertySelector = propertySelector;
        }

        public Action<T> GetPreviousValueAction()
        {
            return PropertySelector.ToAction(PreviousValue);
        }

        public Action<T> GetValueAction()
        {
            return PropertySelector.ToAction(Value);
        }
    }
}