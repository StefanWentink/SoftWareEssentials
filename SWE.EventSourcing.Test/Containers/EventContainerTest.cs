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

    public class EventContainerTest
    {
        private static Product GetDefaultProduct()
        {
            var product = ProductFactory.GetProduct("original");
            AssertProduct(product, 1.0, 10, 10);
            return product;
        }

        private static EventCollection<Product, string> GetDefaultEventCollection(Guid productId)
        {
            var priceChanges = ProductFactory.GetProductPriceChanges(productId).ToList();
            var stockMutationChanges = ProductFactory.GetProductStockMutations(productId).ToList();

            var changes = ChangeFactory.ToOrderedChangeEvent<Product, ProductPriceChange, string , double, DateTime>(
                x => x.Price,
                0,
                priceChanges,
                x => x.Id,
                x => x.Price,
                x => x.ChangeDate).Cast<IEvent<Product, string>>().ToList();

            changes.AddRange(MutationFactory.ToOrderedMutationEvent<Product, ProductStockMutation, string, int, DateTimeOffset>(
                x => x.Available,
                stockMutationChanges,
                x => x.Id,
                x => x.Amount,
                x => x.OrderDate).Cast<IEvent<Product, string>>());

            changes.AddRange(MutationFactory.ToOrderedMutationEvent<Product, ProductStockMutation, string, int, DateTimeOffset>(
                x => x.InStock,
                stockMutationChanges,
                x => x.Id,
                x => x.Amount,
                x => x.DeliveryDate).Cast<IEvent<Product, string>>());

            var eventCollection = new EventCollection<Product, string>(changes);
            eventCollection.Count.Should().Be(changes.Count);

            return eventCollection;
        }

        [Theory]
        [Category("EventContainer")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Insert_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Contains(item).Should().BeFalse();

            eventContainer.Insert(index, item);

            product.Code.Should().Be("original");
            eventContainer.Contains(item).Should().BeTrue();
            eventContainer.IndexOf(item).Should().Be(index);
            eventContainer.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("EventContainer")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void InsertAndApply_Should_InsertAtIndex_With_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Contains(item).Should().BeFalse();

            eventContainer.InsertAndApply(index, item, product);

            product.Code.Should().Be("new");
            eventContainer.Contains(item).Should().BeTrue();
            eventContainer.IndexOf(item).Should().Be(index);
            eventContainer.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("EventContainer")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Insert(index, item);
            eventContainer.Count.Should().Be(count + 1);
            eventContainer.Contains(item).Should().BeTrue();

            eventContainer.RemoveAt(index);

            product.Code.Should().Be("original");
            eventContainer.Contains(item).Should().BeFalse();
            eventContainer.Count.Should().Be(count);
        }

        [Theory]
        [Category("EventContainer")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAndRevert_Should_RemoveAtIndex_With_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Insert(index, item);
            eventContainer.Count.Should().Be(count + 1);
            eventContainer.Contains(item).Should().BeTrue();

            eventContainer.RemoveAndRevert(index, product);

            product.Code.Should().Be("old");
            eventContainer.Contains(item).Should().BeFalse();
            eventContainer.Count.Should().Be(count);
        }

        [Theory]
        [Category("EventContainer")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveIfPresent_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Insert(index, item);
            eventContainer.Count.Should().Be(count + 1);
            eventContainer.Contains(item).Should().BeTrue();

            eventContainer.Remove(item).Should().BeTrue();
            eventContainer.Contains(item).Should().BeFalse();
            eventContainer.Remove(item).Should().BeFalse();

            product.Code.Should().Be("original");
            eventContainer.Count.Should().Be(count);
        }

        [Fact]
        [Category("EventContainer")]
        public void RemoveAndRevert_Should_RemoveIfPresent_With_EffectingItem()
        {
            var product = GetDefaultProduct();
            var eventContainer = GetDefaultEventContainer(product.Id);
            var count = eventContainer.Count;

            var item = new EventStub();
            eventContainer.Add(item);
            eventContainer.Count.Should().Be(count + 1);
            eventContainer.Contains(item).Should().BeTrue();

            eventContainer.RemoveAndRevert(item, product).Should().BeTrue();
            eventContainer.Contains(item).Should().BeFalse();
            eventContainer.RemoveAndRevert(item, product).Should().BeFalse();

            product.Code.Should().Be("old");
            eventContainer.Count.Should().Be(count);
        }

        private static void AssertProduct(Product product, double price, int available, int inStock)
        {
            product.Price.Should().BeApproximately(price, 0.000001);
            product.Available.Should().Be(available);
            product.InStock.Should().Be(inStock);
        }
    }
}
