namespace SWE.EventSourcing.Test.Containers
{
    using FluentAssertions;
    using SWE.EventSourcing.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.EventSourcing.Test.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventContainerTest : BasicEventContainerTest<
        EventContainer<Product, Guid, string>,
        EventCollection<Product, string>,
        Product,
        Guid,
        string>
    {
        protected internal override Product DefaultItem => ProductFactory.GetProduct("original");

        protected internal override IEvent<Product, string> NewEvent => new EventStub();

        protected override void AssertItemApplied(Product item)
        {
            item.Code.Should().Be("new");
        }

        protected override void AssertItemOriginal(Product item)
        {
            item.Code.Should().Be("original");
        }

        protected override void AssertItemReverted(Product item)
        {
            item.Code.Should().Be("old");
        }

        protected internal override EventCollection<Product, string> GetDefaultEventCollection(Guid itemId)
        {
            var changes = ProductFactory.GetChanges(itemId);

            var eventCollection = new EventCollection<Product, string>(changes);
            eventCollection.Count.Should().Be(changes.Count);

            return eventCollection;
        }

        protected internal override EventContainer<Product, Guid, string> GetDefaultEventContainer(IEnumerable<Guid> itemIds)
        {
            var list = itemIds
                .Select(x => new KeyValuePair<Guid, EventCollection<Product, string>>(x, new EventCollection<Product, string>(new List<IEvent<Product, string>>())))
                .ToList();
            return new EventContainer<Product, Guid, string>(list);
        }
    }
}