namespace SWE.EventSourcing.Interfaces.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;

    /// <summary>
    /// Collection of events based on type <see cref="T"/>
    /// </summary>
    /// <typeparam name="TItemEvents"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"><see cref="IKey{TKey}"/> of <see cref="T"/></typeparam>
    /// <typeparam name="TEventKey"><see cref="IKey{TKey}"/> of <see cref="TItemEvents"/></typeparam>
    public interface IBasicEventContainer<TItemEvents, T, TKey, TEventKey>
        where TItemEvents : IEventCollection<T, TEventKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
    {
        TItemEvents ItemEvents(T value);

        bool Contains(TKey id);

        TItemEvents ItemEvents(TKey id);

        void ApplyAll(T value);

        /// <summary>
        /// Applies <see cref="Events"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        void RevertAll(T value);

        void Add(IEvent<T, TEventKey> item, T value);

        void Add(IEvent<T, TEventKey> item, TKey key);

        void AddAndApply(IEvent<T, TEventKey> item, T value);

        void Clear(T value);

        void Clear(TKey key);

        void ClearAndRevert(T value);

        bool Remove(IEvent<T, TEventKey> item, T value);

        bool Remove(IEvent<T, TEventKey> item, TKey key);

        bool RemoveAndRevert(IEvent<T, TEventKey> item, T value);
    }
}