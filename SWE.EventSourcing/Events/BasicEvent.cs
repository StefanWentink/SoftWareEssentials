namespace SWE.EventSourcing.Events
{
    using SWE.Collection.Extensions;
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public abstract class BasicEvent<T, TKey> : IEvent<T, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public IEnumerable<IPropertyAction<T>> PropertyActions { get; }

        protected BasicEvent(TKey id, IPropertyAction<T> propertyAction)
            : this(id, new List<IPropertyAction<T>> { propertyAction })
        {
        }

        protected BasicEvent(TKey id, IEnumerable<IPropertyAction<T>> propertyActions)
        {
            if (propertyActions.IsNullOrEmpty())
            {
                throw new ArgumentException($"{nameof(BasicEvent<T, TKey>)} needs at least one {nameof(IPropertyAction<T>)}.", nameof(propertyActions));
            }

            Id = id;
            PropertyActions = propertyActions;
        }
    }
}