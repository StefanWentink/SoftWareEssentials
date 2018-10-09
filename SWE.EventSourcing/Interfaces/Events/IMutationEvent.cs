using System;

namespace SWE.EventSourcing.Interfaces.Events
{
    public interface IMutationEvent<T, out TKey> : IEvent<T, TKey>
        where TKey : IEquatable<TKey>
    {
    }
}