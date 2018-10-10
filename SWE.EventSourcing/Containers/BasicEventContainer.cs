namespace SWE.EventSourcing.Containers
{
    using MoreLinq;
    using SWE.EventSourcing.EventArgs;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Collection of events based on type <see cref="T"/>
    /// </summary>
    /// <typeparam name="TItemEvents"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"><see cref="IKey{TKey}"/> of <see cref="T"/></typeparam>
    /// <typeparam name="TEventKey"><see cref="IKey{TKey}"/> of <see cref="TItemEvents"/></typeparam>
    public abstract class BasicEventContainer<TItemEvents, T, TKey, TEventKey>
        : IBasicEventContainer<TItemEvents, T, TKey, TEventKey>
        , IEventSourcingHandler<T, TEventKey>
        , IDisposable
        where TItemEvents : IEventCollection<T, TEventKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
    {
        /// <inheritdoc></inheritdoc>
        public event EventHandler<EventSourcingArgs<T, TEventKey>> EventRemoved;

        /// <inheritdoc></inheritdoc>
        public event EventHandler<EventSourcingArgs<T, TEventKey>> EventAdded;

        protected bool IsDisposed { get; private set; }

        private Dictionary<TKey, TItemEvents> ItemEventsContainer { get; }
            = new Dictionary<TKey, TItemEvents>();

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
            itemEvents.ForEach(x => AddItemEvents(x.key, x.itemEvents));
        }

        protected BasicEventContainer(List<KeyValuePair<TKey, TItemEvents>> itemEvents)
        {
            itemEvents.ForEach(x => AddItemEvents(x.Key, x.Value));
        }

        protected BasicEventContainer(Dictionary<TKey, TItemEvents> items)
        {
            items.ForEach(x => AddItemEvents(x.Key, x.Value));
        }

        public virtual TItemEvents ItemEvents(T value)
        {
            return ItemEvents(value.Id);
        }

        public virtual bool Contains(TKey id)
        {
            return ItemEventsContainer.ContainsKey(id);
        }

        public virtual TItemEvents ItemEvents(TKey id)
        {
            if (!Contains(id))
            {
                AddItemEvents(id, CreateItemEvents());
            }

            return ItemEventsContainer[id];
        }

        protected abstract TItemEvents CreateItemEvents();

        private void AddItemEvents(TKey key, TItemEvents itemEvents)
        {
            SetUpEventHandlers(itemEvents);
            ItemEventsContainer.Add(key, itemEvents);
        }

        private void SetUpEventHandlers(TItemEvents itemEvents)
        {
            itemEvents.EventAdded += ItemEvents_EventAdded;
            itemEvents.EventRemoved += ItemEvents_EventRemoved;
        }

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

        public void Add(IEvent<T, TEventKey> item, T value)
        {
            ItemEvents(value).Add(item);
        }

        public void Add(IEvent<T, TEventKey> item, TKey key)
        {
            ItemEvents(key).Add(item);
        }

        public virtual void AddAndApply(IEvent<T, TEventKey> item, T value)
        {
            ItemEvents(value).AddAndApply(item, value);
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

        public bool Remove(IEvent<T, TEventKey> item, T value)
        {
            return ItemEvents(value).Remove(item);
        }

        public bool Remove(IEvent<T, TEventKey> item, TKey key)
        {
            return ItemEvents(key).Remove(item);
        }

        public bool RemoveAndRevert(IEvent<T, TEventKey> item, T value)
        {
            return ItemEvents(value).RemoveAndRevert(item, value);
        }

        private void ItemEvents_EventRemoved(object sender, EventSourcingArgs<T, TEventKey> e)
        {
            if (e.Event != default)
            {
                OnEventRemoved(e);
            }
        }

        private void ItemEvents_EventAdded(object sender, EventSourcingArgs<T, TEventKey> e)
        {
            if (e.Event != default)
            {
                OnEventAdded(e);
            }
        }

        protected virtual void OnEventRemoved(EventSourcingArgs<T, TEventKey> e)
        {
            EventRemoved?.Invoke(this, e);
        }

        protected virtual void OnEventAdded(EventSourcingArgs<T, TEventKey> e)
        {
            EventAdded?.Invoke(this, e);
        }

        ~BasicEventContainer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                if (isDisposing)
                {
                    foreach (var itemEvents in ItemEventsContainer)
                    {
                        itemEvents.Value.EventAdded -= EventAdded;
                        itemEvents.Value.EventRemoved -= EventRemoved;
                        itemEvents.Value.Dispose();
                    }
                }
            }
        }
    }
}