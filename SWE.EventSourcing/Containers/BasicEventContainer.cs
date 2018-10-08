namespace SWE.EventSourcing.Containers
{
    using MoreLinq;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public abstract class BasicEventContainer<TItemEvents, T, TKey>
        where TItemEvents : IItemEvents<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        protected Dictionary<TKey, TItemEvents> ItemEventsContainer { get; } = new Dictionary<TKey, TItemEvents>();

        [Obsolete("only for serialisation")]
        protected BasicEventContainer()
        {
        }

        protected BasicEventContainer(TKey key, TItemEvents itemEvents)
            : this(new List<(TKey, TItemEvents)> { (key, itemEvents) })
        {
        }

        protected BasicEventContainer(IEnumerable<(TKey key, TItemEvents itemEvents)> itemEvents)
        {
            itemEvents.ForEach(x => ItemEventsContainer.Add(x.key, x.itemEvents));
        }

        protected BasicEventContainer(List<KeyValuePair<TKey, TItemEvents>> itemEvents)
        {
            itemEvents.ForEach(x => ItemEventsContainer.Add(x.Key, x.Value));
        }

        protected BasicEventContainer(Dictionary<TKey, TItemEvents> items)
        {
            ItemEventsContainer = items;
        }

        public virtual TItemEvents ItemEvents(T value)
        {
            return ItemEvents(value.Id);
        }

        public virtual bool Contains(TKey id)
        {
            return !ItemEventsContainer.ContainsKey(id);
        }

        public virtual TItemEvents ItemEvents(TKey id)
        {
            if (!Contains(id))
            {
                ItemEventsContainer.Add(id, CreateItemEvents());
            }

            return ItemEventsContainer[id];
        }

        protected abstract TItemEvents CreateItemEvents();

        /// <summary>
        /// Applies <see cref="Events"/> to <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public void ApplyAll(T value)
        {
            ItemEvents(value).ApplyAll(value);
        }

        /// <summary>
        /// Applies <see cref="Events"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public void RevertAll(T value)
        {
            ItemEvents(value).RevertAll(value);
        }

        public void Add(IEvent<T> item, T value)
        {
            ItemEvents(value).Add(item);
        }

        public void Add(IEvent<T> item, TKey key)
        {
            ItemEvents(key).Add(item);
        }

        public virtual void AddAndApply(IEvent<T> item, T value)
        {
            ItemEvents(value).Add(item);
        }

        public void Clear(T value)
        {
            ItemEvents(value).Clear();
        }

        public void Clear(TKey key)
        {
            ItemEvents(key).Clear();
        }

        public virtual void ClearAndRevert(T value)
        {
            ItemEvents(value).ClearAndRevert(value);
        }

        public bool Remove(IEvent<T> item, T value)
        {
            return ItemEvents(value).Remove(item);
        }

        public bool Remove(IEvent<T> item, TKey key)
        {
            return ItemEvents(key).Remove(item);
        }

        public bool RemoveAndRevert(IEvent<T> item, T value)
        {
            return ItemEvents(value).RemoveAndRevert(item, value);
        }
    }
}