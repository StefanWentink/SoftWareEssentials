namespace SWE.EventSourcing.Interfaces
{
    using System;

    public interface IOrdered<TOrder>
        where TOrder : IComparable<TOrder>
    {
        TOrder Order { get; set; }
    }
}