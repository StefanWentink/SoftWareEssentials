namespace SWE.EventSourcing.EventArgs
{
    using SWE.EventSourcing.Interfaces.Events;
    using System;

    /// <summary>
    /// To broadcast <see cref="IEvent<T, TKey>"/> operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EventSourcingArgs<T, TKey> : EventArgs
        where TKey : IEquatable<TKey>
    {
        public IEvent<T, TKey> Event { get; }

        public EventSourcingArgs(IEvent<T, TKey> @event)
        {
            Event = @event;
        }
    }
}