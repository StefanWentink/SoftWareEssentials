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
    using SWE.EventSourcing.Test.Extensions;
    using SWE.EventSourcing.Events.Mutation;
    using SWE.EventSourcing.Models;
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Events.Change;

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

        [Theory]
        [Category("EventCollection")]
        [InlineData(2017, 1, 1, 1.0, 10)]
        [InlineData(2018, 1, 1, 1.2, 10)]
        [InlineData(2018, 3, 1, 1.2, 5)]
        [InlineData(2018, 3, 10, 1.2, 5)]
        [InlineData(2018, 3, 20, 1.2, 1)]
        [InlineData(2019, 1, 1, 1.8, 12)]
        public void ApplyAll_Should_TakeOrderInConsideration_With_Available(
            int year,
            int month,
            int day,
            double expectedPrice,
            int expectedInStock)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Contains(newEvent).Should().BeFalse();

            eventCollection.ApplyAll(item, new DateTime(year, month, day));

            item.AssertProduct(expectedPrice, 12, expectedInStock);
        }

        [Fact]
        [Category("EventCollection")]
        public void AddAndApply_Should_ApplyAllEvents_When_Change()
        {
            var item = DefaultItem;
            var eventCollection = new OrderedEventCollection<Product, string, DateTime>(new List<IEvent<Product, string>>());

            var firstEvent = new ChangeEvent<Product, string>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(10, 1,  x => x.Available)
                });

            var secondEvent = new ChangeEvent<Product, string>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(1, 2, x => x.Available)
                });

            var thirdEvent = new ChangeEvent<Product, string>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(2, 3, x => x.Available)
                });

            eventCollection.AddAndApply(firstEvent, item);
            eventCollection.Contains(firstEvent).Should().BeTrue();
            item.AssertProduct(1.0, 1, 10);

            eventCollection.AddAndApply(secondEvent, item);
            eventCollection.Contains(secondEvent).Should().BeTrue();
            item.AssertProduct(1.0, 2, 10);

            eventCollection.AddAndApply(thirdEvent, item);
            eventCollection.Contains(thirdEvent).Should().BeTrue();
            item.AssertProduct(1.0, 3, 10);
        }

        [Fact]
        [Category("EventCollection")]
        public void AddAndApply_Should_ApplyAllEvents_When_Mutation()
        {
            var item = DefaultItem;
            var eventCollection = new OrderedEventCollection<Product, string, DateTime>(new List<IEvent<Product, string>>());

            var firstEvent = new OrderedMutationEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(1, x => x.Available)
                },
                new DateTime(2018, 3, 3));

            var secondEvent = new OrderedMutationEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(2, x => x.Available)
                },
                new DateTime(2018, 3, 3));

            var thirdEvent = new OrderedMutationEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(3, x => x.Available)
                },
                new DateTime(2018, 3, 2));

            eventCollection.AddAndApply(firstEvent, item);
            eventCollection.Contains(firstEvent).Should().BeTrue();
            item.AssertProduct(1.0, 11, 10);

            eventCollection.AddAndApply(secondEvent, item);
            eventCollection.Contains(secondEvent).Should().BeTrue();
            item.AssertProduct(1.0, 13, 10);

            eventCollection.AddAndApply(thirdEvent, item);
            eventCollection.Contains(thirdEvent).Should().BeTrue();
            item.AssertProduct(1.0, 16, 10);
        }

        [Fact]
        [Category("EventCollection")]
        public void AddAndApply_Should_ApplyAllEvents_When_OrderedNoMutation()
        {
            var item = DefaultItem;
            var eventCollection = new OrderedEventCollection<Product, string, DateTime>(new List<IEvent<Product, string>>());

            var firstEvent = new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(10, 1,  x => x.Available)
                },
                new DateTime(2018, 3, 3));

            var secondEvent = new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(1, 2, x => x.Available)
                },
                new DateTime(2018, 3, 3));

            var thirdEvent = new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyChange<Product, int>(2, 3, x => x.Available)
                },
                new DateTime(2018, 3, 2));

            eventCollection.AddAndApply(firstEvent, item);
            eventCollection.Contains(firstEvent).Should().BeTrue();
            item.AssertProduct(1.0, 1, 10);

            eventCollection.AddAndApply(secondEvent, item);
            eventCollection.Contains(secondEvent).Should().BeTrue();
            item.AssertProduct(1.0, 2, 10);

            eventCollection.AddAndApply(thirdEvent, item);
            eventCollection.Contains(thirdEvent).Should().BeTrue();
            item.AssertProduct(1.0, 2, 10);
        }

        public void TryRevertLast_Should_RevertChangesWithMaxOrder_And_ReturnNumberOfEventsRevertedANdTheirOrder()
        {
            var item = DefaultItem;

            var eventList = new List<IEvent<Product, string>>();

            var highestDate = new DateTime(2018, 3, 3);
            var lowestDate = new DateTime(2018, 3, 2);

            eventList.Add(new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(1, x => x.Available)
                },
                highestDate));

            eventList.Add(new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(2, x => x.Available)
                },
                highestDate));

            eventList.Add(new OrderedChangeEvent<Product, string, DateTime>(
                Guid.NewGuid().ToString(),
                new List<IPropertyAction<Product>> {
                    new PropertyMutation<Product, int>(3, x => x.Available)
                },
                new DateTime(2018, 3, 2)));

            var eventCollection = new OrderedEventCollection<Product, string, DateTime>(eventList);

            eventCollection.TryRevertLast(item, out var _firstOrder).Should().Be(2);
            _firstOrder.Should().Be(highestDate);


            eventCollection.TryRevertLast(item, out var _secondOrder).Should().Be(1);
            _secondOrder.Should().Be(highestDate);

            eventCollection.TryRevertLast(item, out var _thirdOrder).Should().Be(0);
            _secondOrder.Should().Be(default);
        }
    }
}