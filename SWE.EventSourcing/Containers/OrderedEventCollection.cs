namespace SWE.EventSourcing.Containers
{
    using SWE.BasicType.Utilities;
    using SWE.EventSourcing.Extensions;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Collection of events based on type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    public class OrderedEventCollection<T, TKey, TOrder>
        : EventCollection<T, TKey>
        where TKey : IEquatable<TKey>
        where TOrder : IComparable<TOrder>
    {
        private TOrder MaxOrder => Items.Max(x => (x as IOrderedEvent<TKey, TOrder>).Order);

        [Obsolete("only for serialisation")]
        public OrderedEventCollection()
        {
        }

        public OrderedEventCollection(IEvent<T, TKey> item)
            : base(new List<IEvent<T, TKey>> { item })
        {
        }

        public OrderedEventCollection(IList<IEvent<T, TKey>> items)
            : base(items)
        {
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T, TKey}"/>
        /// and all <see cref="IOrderedEvent{TKey, TOrder}"/> up to <see cref="orderSelector"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="orderSelector"></param>
        public void ApplyAll(T value, Func<T, TOrder> orderSelector)
        {
            var order = orderSelector(value);
            ApplyNonOrdered(value);
            ApplyUpToOrder(value, order);
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T, TKey}"/>
        /// and all <see cref="IOrderedEvent{TKey, TOrder}"/> up to <see cref="order"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="order"></param>
        public void ApplyAll(T value, TOrder order)
        {
            ApplyNonOrdered(value);
            ApplyUpToOrder(value, order);
        }

        private void ApplyNonOrdered(T value)
        {
            Items
                .Where(x => x is IOrderedEvent<TKey, TOrder> orderedEvent)
                .Apply(value);
        }

        private void ApplyUpToOrder(T value, TOrder order)
        {
            Items
                .Where(x =>
                    (x is IOrderedEvent<TKey, TOrder> orderedEvent)
                    && CompareUtilities.SmallerOrEqualTo(orderedEvent.Order, order))
                .Apply(value);
        }

        /// <summary>
        /// Adds <see cref="item"/> to list.
        /// Applies <see cref="item"/> if <see cref="IMutationEvent{T}"/>
        /// or  <see cref="item"/> if <see cref="IOrderedEvent{TKey, TOrder}.Order"/> <see cref="CompareUtilities.GreaterOrEqualTo"/> <see cref="MaxOrder"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="item"></param>
        /// <returns>Wether <see cref="item"/> is applied.</returns>
        public override bool AddAndApply(IEvent<T, TKey> item, T value)
        {
            Items.Add(item);

            if ((item is IMutationEvent<T, TKey>)
                 || ((item is IOrderedEvent<TKey, TOrder> orderedEvent) && CompareUtilities.GreaterOrEqualTo(orderedEvent.Order, MaxOrder)))
            {
                item.Apply(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reverts <see cref="IOrderedEvent{TKey, TOrder}"/> with the highest <see cref="IOrderedEvent{TKey, TOrder}.Order"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_order"><see cref="IOrderedEvent{TKey, TOrder}.Order"/> that is reverted.</param>
        /// <returns>Number of events reverted.</returns>
        public int TryRevertLast(T value, out TOrder _order)
        {
            var maxOrder = MaxOrder;
            _order = maxOrder;

            if (maxOrder != default)
            {
                var items = Items
                    .Where(x =>
                        (x is IOrderedEvent<TKey, TOrder> orderedEvent)
                        && orderedEvent.Order.Equals(maxOrder));

                items.Apply(value);

                return items.Count();
            }

            return 0;
        }
    }
}