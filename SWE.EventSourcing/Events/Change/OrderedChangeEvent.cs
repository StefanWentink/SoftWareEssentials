namespace SWE.EventSourcing.Events.Change
{
    using SWE.Collection.Extensions;
    using SWE.EventSourcing.Interfaces;
    using System;
    using System.Collections.Generic;

    public class OrderedMutationEvent<T, TOrder> : BasicChangeEvent<T>
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