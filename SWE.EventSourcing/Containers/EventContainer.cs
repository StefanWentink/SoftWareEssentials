namespace SWE.EventSourcing.Containers
{
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public class EventContainer<T, TKey> : BasicEventContainer<EventCollection<T>, T, TKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        [Obsolete("only for serialisation")]
        public EventContainer()
        {
        }

        public EventContainer(TKey key, EventCollection<T> itemEvents)
            : base(key, itemEvents)
        {
        }

        public EventContainer(IEnumerable<(TKey key, EventCollection<T> itemEvents)> itemEvents)
            : base(itemEvents)
        {
        }

        public EventContainer(List<KeyValuePair<TKey, EventCollection<T>>> itemEvents)
            :base(itemEvents)
        {
        }

        public EventContainer(Dictionary<TKey, EventCollection<T>> items)
            : base(items)
        {
        }

        protected override EventCollection<T> CreateItemEvents()
        {
            return new EventCollection<T>(new List<IEvent<T>>());
        }
    }
}