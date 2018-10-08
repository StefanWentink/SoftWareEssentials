namespace SWE.EventSourcing.Interfaces.Events
{
    using SWE.EventSourcing.Interfaces;
    using System.Collections.Generic;

    public interface IEvent<T>
    {
        IEnumerable<IPropertyAction<T>> PropertyActions { get; }
    }
}