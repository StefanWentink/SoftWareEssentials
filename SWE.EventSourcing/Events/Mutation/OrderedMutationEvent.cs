namespace SWE.EventSourcing.Events.Mutation
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public class OrderedMutationEvent<T, TKey, TOrder>
        : BasicEvent<T, TKey>
        , IMutationEvent<T, TKey>
        , IOrderedEvent<TKey, TOrder>
        where TKey : IEquatable<TKey>
        where TOrder : IComparable<TOrder>
    {
        public TOrder Order { get; set; }

        public OrderedMutationEvent(TKey id, IPropertyAction<T> propertyAction, TOrder order)
            : this(id, new List<IPropertyAction<T>> { propertyAction }, order)
        {
        }

        public OrderedMutationEvent(TKey id, IEnumerable<IPropertyAction<T>> propertyActions, TOrder order)
            : base(id, propertyActions)
        {
            Order = order;
        }
    }
}