namespace SWE.EventSourcing.Events.Mutation
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public class MutationEvent<T, TKey>
        : BasicEvent<T, TKey>
        , IMutationEvent<T, TKey>
        where TKey : IEquatable<TKey>
    {
        public MutationEvent(TKey id, IPropertyAction<T> propertyAction)
            : base(id, new List<IPropertyAction<T>> { propertyAction })
        {
        }

        public MutationEvent(TKey id, IEnumerable<IPropertyAction<T>> propertyActions)
            : base(id, propertyActions)
        {
        }
    }
}