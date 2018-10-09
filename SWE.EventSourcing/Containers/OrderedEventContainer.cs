namespace SWE.EventSourcing.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public class OrderedEventContainer<T, TKey, TEventKey, TOrder>
        : BasicEventContainer<OrderedEventCollection<T, TEventKey, TOrder>, T, TKey, TEventKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
        where TOrder : IComparable<TOrder>
    {
        [Obsolete("only for serialisation")]
        public OrderedEventContainer()
            : base()
        {
        }

        public OrderedEventContainer(TKey key, OrderedEventCollection<T, TEventKey, TOrder> itemEvents)
            : base(key, itemEvents)
        {
        }

        public OrderedEventContainer(IEnumerable<(TKey key, OrderedEventCollection<T, TEventKey, TOrder> itemEvents)> itemEvents)
            : base(itemEvents)
        {
        }

        public OrderedEventContainer(List<KeyValuePair<TKey, OrderedEventCollection<T, TEventKey, TOrder>>> itemEvents)
            : base(itemEvents)
        {
        }

        public OrderedEventContainer(Dictionary<TKey, OrderedEventCollection<T, TEventKey, TOrder>> items)
            : base(items)
        {
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T}"/>
        /// and all <see cref="IOrderedEvent{TEventKey, TOrder}"/> up to <see cref="orderSelector"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderSelector"></param>
        public void ApplyAll(T value, Func<T, TOrder> orderSelector)
        {
            ItemEvents(value).ApplyAll(value, orderSelector);
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T}"/>
        /// and all <see cref="IOrderedEvent{TEventKey, TOrder}"/> up to <see cref="order"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="order"></param>
        public void ApplyAll(T value, TOrder order)
        {
            ItemEvents(value).ApplyAll(value, order);
        }

        /// <summary>
        /// Reverts <see cref="IOrderedEvent{TEventKey, TOrder}"/> with the highest <see cref="IOrderedEvent{TEventKey, TOrder}.Order"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_order"><see cref="IOrderedEvent{TEventKey, TOrder}.Order"/> that is reverted.</param>
        /// <returns>Number of events reverted.</returns>
        public int TryRevertLast(T value, out TOrder _order)
        {
            return ItemEvents(value).TryRevertLast(value, out _order);
        }

        protected override OrderedEventCollection<T, TEventKey, TOrder> CreateItemEvents()
        {
            return new OrderedEventCollection<T, TEventKey, TOrder>(new List<IEvent<T, TEventKey>>());
        }
    }
}