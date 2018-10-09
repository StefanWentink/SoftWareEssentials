namespace SWE.EventSourcing.Interfaces.Containers
{
    using SWE.EventSourcing.EventArgs;
    using System;

    public interface IEventSourcingHandler<T, TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// When <see cref="IEventCollection{T, TKey}"/> is removed.
        /// </summary>
        event EventHandler<EventSourcingArgs<T, TKey>> EventRemoved;

        /// <summary>
        /// When <see cref="IEventCollection{T, TKey}"/> is added.
        /// </summary>
        event EventHandler<EventSourcingArgs<T, TKey>> EventAdded;
    }
}