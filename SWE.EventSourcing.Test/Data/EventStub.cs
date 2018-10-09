namespace SWE.EventSourcing.Test.Data
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.EventSourcing.Models;
    using System;
    using System.Collections.Generic;

    internal class EventStub : IEvent<Product, string>
    {
        public string Id { get; set; }

        public EventStub()
        {
            Id = Guid.NewGuid().ToString();
        }

        public IEnumerable<IPropertyAction<Product>> PropertyActions => new List<IPropertyAction<Product>>
        {
            new PropertyChange<Product, string>("old", "new", x => x.Code)
        };
    }
}