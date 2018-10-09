namespace SWE.EventSourcing.Test.Containers
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.EventSourcing.Containers;
    using SWE.EventSourcing.Test.Data;
    using FluentAssertions;
    using System.Collections.Generic;
    using SWE.EventSourcing.Interfaces.Events;
    using System.Linq;
    using System;
    using SWE.EventSourcing.Factories;
    using SWE.EventSourcing.EventArgs;

    public class OrderedOrderedEventCollectionTest : BasicEventCollectionTest<OrderedEventCollection<Product, string, DateTime>, Product, Guid, string>
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
    }
}
