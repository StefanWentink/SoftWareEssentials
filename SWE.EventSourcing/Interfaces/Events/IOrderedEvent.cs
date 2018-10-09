namespace SWE.EventSourcing.Interfaces.Events
{
    using System;

    public interface IOrderedEvent<out TKey, TOrder>
        where TKey : IEquatable<TKey>
        where TOrder : IComparable<TOrder>
    {
        TOrder Order { get; set; }
    }
}