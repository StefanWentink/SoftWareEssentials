namespace SWE.EventSourcing.Test.Containers
{
    using FluentAssertions;
    using SWE.EventSourcing.Containers;
    using SWE.EventSourcing.Interfaces.Events;
    using SWE.EventSourcing.Test.Data;
    using System;

    public class EventCollectionTest : BasicEventCollectionTest<EventCollection<Product, string>, Product, Guid, string>
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
    }
}