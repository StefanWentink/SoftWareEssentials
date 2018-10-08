namespace SWE.EventSourcing.Test.Data
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.EventSourcing.Models;
    using System.Collections.Generic;

    internal class EventStub : IEvent<Product>
    {
        public IEnumerable<IPropertyAction<Product>> PropertyActions => new List <IPropertyAction<Product>>
        {
            new PropertyChange<Product, string>("old", "new", x => x.Code)
        };
    }
}