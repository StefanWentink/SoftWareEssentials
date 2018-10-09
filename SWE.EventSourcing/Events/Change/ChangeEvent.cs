namespace SWE.EventSourcing.Events.Change
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public class ChangeEvent<T, TKey>
        : BasicEvent<T, TKey>
        , IChangeEvent<T, TKey>
        where TKey : IEquatable<TKey>
    {
        public ChangeEvent(TKey id, IPropertyAction<T> propertyAction)
            : base(id, new List<IPropertyAction<T>> { propertyAction })
        {
        }

        public ChangeEvent(TKey id, IEnumerable<IPropertyAction<T>> propertyActions)
            : base(id, propertyActions)
        {
        }
    }
}