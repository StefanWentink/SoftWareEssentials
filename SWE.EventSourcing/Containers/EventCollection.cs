namespace SWE.EventSourcing.Containers
{
    using SWE.EventSourcing.Extensions;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EventCollection<T> : IItemEvents<T>
    {
        protected IList<IEvent<T>> Items { get; }

        public int Count => Items.Count;

        public bool IsReadOnly => Items.IsReadOnly;

        [Obsolete("only for serialisation")]
        public EventCollection()
        {
        }

        public EventCollection(IEvent<T> item)
            : this(new List<IEvent<T>> { item })
        {
        }

        public EventCollection(IList<IEvent<T>> items)
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
        public IEvent<T> this[int index]
        {
            get { return Items[index]; }
            set { Items.Insert(index, value); }
        }

        /// <inheritdoc/>
        public IEnumerator<IEvent<T>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(IEvent<T> item)
        {
            return Items.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, IEvent<T> item)
        {
            Items.Insert(index, item);
        }

        public virtual void InsertAndApply(int index, IEvent<T> item, T value)
        {
            item.Apply(value);
            Insert(index, item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public virtual void RemoveAndRevert(int index, T value)
        {
            Items[index].Revert(value);
            RemoveAt(index);
        }

        /// <inheritdoc/>
        public void Add(IEvent<T> item)
        {
            Items.Add(item);
        }

        public virtual bool AddAndApply(IEvent<T> item, T value)
        {
            item.Apply(value);
            Add(item);
            return true;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Items.Clear();
        }

        public virtual void ClearAndRevert(T value)
        {
            RevertAll(value);
            Clear();
        }

        /// <inheritdoc/>
        public bool Contains(IEvent<T> item)
        {
            return Items.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(IEvent<T>[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(IEvent<T> item)
        {
            return Items.Remove(item);
        }

        public bool RemoveAndRevert(IEvent<T> item, T value)
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
    }
}