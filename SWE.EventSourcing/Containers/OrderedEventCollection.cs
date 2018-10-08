namespace SWE.EventSourcing.Containers
{
    using SWE.BasicType.Utilities;
    using SWE.EventSourcing.Extensions;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderedEventCollection<T, TOrder> : EventCollection<T>
        where TOrder : IComparable<TOrder>
    {
        private TOrder MaxOrder => Items.Max(x => (x as IOrderedEvent<TOrder>).Order);

        [Obsolete("only for serialisation")]
        public OrderedEventCollection()
        {
        }

        public OrderedEventCollection(IEvent<T> item)
            : base(new List<IEvent<T>> { item })
        {
        }

        public OrderedEventCollection(IList<IEvent<T>> items)
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
            var order = orderSelector(value);
            ApplyNonOrdered(value);
            ApplyUpToOrder(value, order);
        }

        /// <summary>
        /// Apply all <see cref="IEvent{T}"/>
        /// and all <see cref="IOrderedEvent{TOrder}"/> up to <see cref="order"/>.
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
                .Where(x => x is IOrderedEvent<TOrder> orderedEvent)
                .Apply(value);
        }

        private void ApplyUpToOrder(T value, TOrder order)
        {
            Items
                .Where(x =>
                    (x is IOrderedEvent<TOrder> orderedEvent)
                    && CompareUtilities.SmallerOrEqualTo(orderedEvent.Order, order))
                .Apply(value);
        }

        /// <summary>
        /// Adds <see cref="item"/> to list.
        /// Applies <see cref="item"/> if <see cref="IMutationEvent{T}"/>
        /// or  <see cref="item"/> if <see cref="IOrderedEvent{TOrder}.Order"/> <see cref="CompareUtilities.GreaterOrEqualTo"/> <see cref="MaxOrder"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="item"></param>
        /// <returns>Wether <see cref="item"/> is applied.</returns>
        public override bool AddAndApply(IEvent<T> item, T value)
        {
            Items.Add(item);

            if ((item is IMutationEvent<T>)
                 || ((item is IOrderedEvent<TOrder> orderedEvent) && CompareUtilities.GreaterOrEqualTo(orderedEvent.Order, MaxOrder)))
            {
                item.Apply(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reverts <see cref="IOrderedEvent{TOrder}"/> with the highest <see cref="IOrderedEvent{TOrder}.Order"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="_order"><see cref="IOrderedEvent{TOrder}.Order"/> that is reverted.</param>
        /// <returns>Number of events reverted.</returns>
        public int TryRevertLast(T value, out TOrder _order)
        {
            var maxOrder = MaxOrder;
            _order = maxOrder;

            if (maxOrder != default)
            {
                var items = Items
                    .Where(x =>
                        (x is IOrderedEvent<TOrder> orderedEvent)
                        && orderedEvent.Order.Equals(maxOrder));

                items.Apply(value);

                return items.Count();
            }

            return 0;
        }
    }
}