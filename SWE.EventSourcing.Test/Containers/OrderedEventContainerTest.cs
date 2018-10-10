namespace SWE.EventSourcing.Test.Containers
{
    using FluentAssertions;
    using SWE.EventSourcing.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.EventSourcing.Test.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderedEventContainerTest : BasicEventContainerTest<
        OrderedEventContainer<Product, Guid, string, DateTime>,
        OrderedEventCollection<Product, string, DateTime>,
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

        protected internal override OrderedEventCollection<Product, string, DateTime> GetDefaultEventCollection(Guid itemId)
        {
            var changes = ProductFactory.GetChanges(itemId);

            var eventCollection = new OrderedEventCollection<Product, string, DateTime>(changes);
            eventCollection.Count.Should().Be(changes.Count);

            return eventCollection;
        }

        protected internal override OrderedEventContainer<Product, Guid, string, DateTime> GetDefaultEventContainer(IEnumerable<Guid> itemIds)
        {
            var list = itemIds
                .Select(x => new KeyValuePair<Guid, OrderedEventCollection<Product, string, DateTime>>(x, new OrderedEventCollection<Product, string, DateTime>(new List<IEvent<Product, string>>())))
                .ToList();
            return new OrderedEventContainer<Product, Guid, string, DateTime>(list);
        }
    }
}