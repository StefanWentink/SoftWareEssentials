namespace SWE.EventSourcing.Models
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Expression.Extensions;
    using System;
    using System.Linq.Expressions;

    public class PropertyChange<T, TValue>
        : IPropertyChange<TValue>
        , IPropertyAction<T>
    {
        private Action<T> _revertValueAction;

        private Action<T> _valueAction;

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
            PropertySelector = propertySelector;
        }

        public Action<T> GetRevertValueAction()
        {
            return _revertValueAction ?? (_revertValueAction = PropertySelector.ToSetAction(PreviousValue));
        }

        public Action<T> GetApplyValueAction()
        {
            return _valueAction ?? (_valueAction = PropertySelector.ToSetAction(Value));
        }
    }
}