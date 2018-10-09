namespace SWE.EventSourcing.Containers
{
    using MoreLinq;
    using SWE.EventSourcing.EventArgs;
    using SWE.EventSourcing.Extensions;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Collection of events based on type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"><see cref="IKey{TKey}"/> of <see cref="IEventCollection{T, TKey}"/></typeparam>
    public class EventCollection<T, TKey>
        : IEventCollection<T, TKey>
        where TKey : IEquatable<TKey>
    {
        /// <inheritdoc></inheritdoc>
        public event EventHandler<EventSourcingArgs<T, TKey>> EventRemoved;

        /// <inheritdoc></inheritdoc>
        public event EventHandler<EventSourcingArgs<T, TKey>> EventAdded;

        protected bool IsDisposed { get; private set; }

        protected IList<IEvent<T, TKey>> Items { get; }

        public int Count => Items.Count;

        public bool IsReadOnly => Items.IsReadOnly;

        [Obsolete("only for serialisation")]
        public EventCollection()
        {
        }

        public EventCollection(IEvent<T, TKey> item)
            : this(new List<IEvent<T, TKey>> { item })
        {
        }

        public EventCollection(IList<IEvent<T, TKey>> items)
        {
            Items = items;
        }

        /// <summary>
        /// Applies <see cref="Items"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public void ApplyAll(T value)
        {
            Items.Apply(value);
        }

        /// <summary>
        /// Reverts <see cref="Items"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public void RevertAll(T value)
        {
            Items.Revert(value);
        }

        /// <inheritdoc/>
        public IEvent<T, TKey> this[int index]
        {
            get { return Items[index]; }
            set { Items.Insert(index, value); }
        }

        /// <inheritdoc/>
        public IEnumerator<IEvent<T, TKey>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(IEvent<T, TKey> item)
        {
            return Items.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, IEvent<T, TKey> item)
        {
            OnEventAdded(new EventSourcingArgs<T, TKey>(item));
            Items.Insert(index, item);
        }

        public virtual void InsertAndApply(int index, IEvent<T, TKey> item, T value)
        {
            item.Apply(value);
            Insert(index, item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            OnEventRemoved(new EventSourcingArgs<T, TKey>(Items[index]));
            Items.RemoveAt(index);
        }

        public virtual void RemoveAndRevert(int index, T value)
        {
            Items[index].Revert(value);
            RemoveAt(index);
        }

        /// <inheritdoc/>
        public void Add(IEvent<T, TKey> item)
        {
            OnEventAdded(new EventSourcingArgs<T, TKey>(item));
            Items.Add(item);
        }

        public virtual bool AddAndApply(IEvent<T, TKey> item, T value)
        {
            if (Contains(item))
            {
                return false;
            }

            item.Apply(value);
            Add(item);

            return true;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Items.ForEach(x => OnEventRemoved(new EventSourcingArgs<T, TKey>(x)));
            Items.Clear();
        }

        public virtual void ClearAndRevert(T value)
        {
            RevertAll(value);
            Clear();
        }

        /// <inheritdoc/>
        public bool Contains(IEvent<T, TKey> item)
        {
            return Items.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(IEvent<T, TKey>[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(IEvent<T, TKey> item)
        {
            var result = Items.Remove(item);

            if (result)
            {
                OnEventRemoved(new EventSourcingArgs<T, TKey>(item));
            }

            return result;
        }

        public bool RemoveAndRevert(IEvent<T, TKey> item, T value)
        {
            if (Remove(item))
            {
                item.Revert(value);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected virtual void OnEventRemoved(EventSourcingArgs<T, TKey> e)
        {
            EventRemoved?.Invoke(this, e);
        }

        protected virtual void OnEventAdded(EventSourcingArgs<T, TKey> e)
        {
            EventAdded?.Invoke(this, e);
        }

        ~EventCollection()
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
                    Items?.Clear();
                }
            }
        }
    }
}