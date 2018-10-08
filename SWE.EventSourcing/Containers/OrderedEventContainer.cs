namespace SWE.EventSourcing.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public class OrderedEventContainer<T, TOrder, TKey> : BasicEventContainer<OrderedEventCollection<T, TOrder>, T, TKey>
        where T : IKey<TKey>
        where TOrder : IComparable<TOrder>
        where TKey : IEquatable<TKey>
    {
        [Obsolete("only for serialisation")]
        public OrderedEventContainer()
            :base()
        {
        }

        public OrderedEventContainer(TKey key, OrderedEventCollection<T, TOrder> itemEvents)
            : base(key, itemEvents)
        {
        }

        public OrderedEventContainer(IEnumerable<(TKey key, OrderedEventCollection<T, TOrder> itemEvents)> itemEvents)
            : base(itemEvents)
        {
        }

        public OrderedEventContainer(List<KeyValuePair<TKey, OrderedEventCollection<T, TOrder>>> itemEvents)
            : base(itemEvents)
        {
        }

        public OrderedEventContainer(Dictionary<TKey, OrderedEventCollection<T, TOrder>> items)
            : base(items)
        {
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T}"/>
        /// and all <see cref="IOrderedEvent{TOrder}"/> up to <see cref="orderSelector"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderSelector"></param>
        public void ApplyAll(T value, Func<T, TOrder> orderSelector)
        {
            ItemEvents(value).ApplyAll(value, orderSelector);
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T}"/>
        /// and all <see cref="IOrderedEvent{TOrder}"/> up to <see cref="order"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="order"></param>
        public void ApplyAll(T value, TOrder order)
        {
            ItemEvents(value).ApplyAll(value, order);
        }

        /// <summary>
        /// Reverts <see cref="IOrderedEvent{TOrder}"/> with the highest <see cref="IOrderedEvent{TOrder}.Order"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_order"><see cref="IOrderedEvent{TOrder}.Order"/> that is reverted.</param>
        /// <returns>Number of events reverted.</returns>
        public int TryRevertLast(T value, out TOrder _order)
        {
            return ItemEvents(value).TryRevertLast(value, out _order);
        }

        protected override OrderedEventCollection<T, TOrder> CreateItemEvents()
        {
            return new OrderedEventCollection<T, TOrder>(new List<IEvent<T>>());
        }
    }
}