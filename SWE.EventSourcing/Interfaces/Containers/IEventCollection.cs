namespace SWE.EventSourcing.Interfaces.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public interface IEventCollection<T, TKey>
        : IList<IEvent<T, TKey>>
        , IEventSourcingHandler<T, TKey>
        , IDisposable
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Applies <see cref="Items"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        void ApplyAll(T value);

        /// <summary>
        /// Reverts <see cref="Items"/> on <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        void RevertAll(T value);

        void InsertAndApply(int index, IEvent<T, TKey> item, T value);

        void RemoveAndRevert(int index, T value);

        bool AddAndApply(IEvent<T, TKey> item, T value);

        void ClearAndRevert(T value);

        bool RemoveAndRevert(IEvent<T, TKey> item, T value);
    }
}