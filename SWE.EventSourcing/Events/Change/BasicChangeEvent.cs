namespace SWE.EventSourcing.Events.Change
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Collection.Extensions;
    using System;
    using System.Collections.Generic;

    public abstract class BasicChangeEvent<T>
    {
        public IEnumerable<IPropertyAction<T>> PropertyActions { get; }

        protected BasicChangeEvent(IPropertyAction<T> propertyAction)
            : this(new List<IPropertyAction<T>> { propertyAction })
        {
        }

        protected BasicChangeEvent(IEnumerable<IPropertyAction<T>> propertyActions)
        {
            if (propertyActions.IsNullOrEmpty())
            {
                throw new ArgumentException($"{nameof(BasicChangeEvent<T>)} needs at least one {nameof(IPropertyAction<T>)}.", nameof(propertyActions));
            }

            PropertyActions = propertyActions;
        }
    }
}