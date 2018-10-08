namespace SWE.EventSourcing.Events
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Collection.Extensions;
    using System;
    using System.Collections.Generic;
    using SWE.EventSourcing.Interfaces.Events;

    public abstract class BasicEvent<T> : IEvent<T>
    {
        public IEnumerable<IPropertyAction<T>> PropertyActions { get; }

        protected BasicEvent(IPropertyAction<T> propertyAction)
            : this(new List<IPropertyAction<T>> { propertyAction })
        {
        }

        protected BasicEvent(IEnumerable<IPropertyAction<T>> propertyActions)
        {
            if (propertyActions.IsNullOrEmpty())
            {
                throw new ArgumentException($"{nameof(BasicEvent<T>)} needs at least one {nameof(IPropertyAction<T>)}.", nameof(propertyActions));
            }

            PropertyActions = propertyActions;
        }
    }
}