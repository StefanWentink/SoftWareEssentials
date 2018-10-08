namespace SWE.EventSourcing.Events.Change
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public class OrderedChangeEvent<T, TOrder>
        : BasicEvent<T>
        , IChangeEvent<T>
        , IOrderedEvent<TOrder>
        where TOrder : IComparable<TOrder>
    {
        public TOrder Order { get; set; }

        public OrderedChangeEvent(IPropertyAction<T> propertyAction, TOrder order)
            : this(new List<IPropertyAction<T>> { propertyAction }, order)
        {
        }

        public OrderedChangeEvent(IEnumerable<IPropertyAction<T>> propertyActions, TOrder order)
            : base(propertyActions)
        {
            Order = order;
        }
    }
}