using System;

namespace SWE.EventSourcing.Interfaces.Events
{
    public interface IChangeEvent<T, out TKey> : IEvent<T, TKey>
        where TKey : IEquatable<TKey>
    {
    }
}