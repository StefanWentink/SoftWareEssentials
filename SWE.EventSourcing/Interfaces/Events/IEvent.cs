namespace SWE.EventSourcing.Interfaces.Events
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public interface IEvent<T, out TKey> : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<IPropertyAction<T>> PropertyActions { get; }
    }
}