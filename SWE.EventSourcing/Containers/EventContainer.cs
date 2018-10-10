namespace SWE.EventSourcing.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public class EventContainer<T, TKey, TEventKey>
        : BasicEventContainer<EventCollection<T, TEventKey>, T, TKey, TEventKey>
        , IDisposable
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
    {
        [Obsolete("only for serialisation")]
        public EventContainer()
        {
        }

        public EventContainer(TKey key, EventCollection<T, TEventKey> itemEvents)
            : base(key, itemEvents)
        {
        }

        public EventContainer(IEnumerable<(TKey key, EventCollection<T, TEventKey> itemEvents)> itemEvents)
            : base(itemEvents)
        {
        }

        public EventContainer(List<KeyValuePair<TKey, EventCollection<T, TEventKey>>> itemEvents)
            : base(itemEvents)
        {
        }

        public EventContainer(Dictionary<TKey, EventCollection<T, TEventKey>> items)
            : base(items)
        {
        }

        protected override EventCollection<T, TEventKey> CreateItemEvents()
        {
            return new EventCollection<T, TEventKey>(new List<IEvent<T, TEventKey>>());
        }
    }
}