namespace SWE.EventSourcing.Events.Mutation
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public class OrderedMutationEvent<T, TOrder>
        : BasicEvent<T>
        , IMutationEvent<T>
        , IOrderedEvent<TOrder>
        where TOrder : IComparable<TOrder>
    {
        public TOrder Order { get; set; }

        public OrderedMutationEvent(IPropertyAction<T> propertyAction, TOrder order)
            : this(new List<IPropertyAction<T>> { propertyAction }, order)
        {
        }

        public OrderedMutationEvent(IEnumerable<IPropertyAction<T>> propertyActions, TOrder order)
            : base(propertyActions)
        {
            Order = order;
        }
    }
}