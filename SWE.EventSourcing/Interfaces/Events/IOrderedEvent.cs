namespace SWE.EventSourcing.Interfaces.Events
{
    using System;

    public interface IOrderedEvent<TOrder>
        where TOrder : IComparable<TOrder>
    {
        TOrder Order { get; set; }
    }
}