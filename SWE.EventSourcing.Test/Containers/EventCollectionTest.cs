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

    public class EventCollectionTest
    {
        private static Product GetDefaultProduct()
        {
            var product = ProductFactory.GetProduct("original");
            AssertProduct(product, 1.0, 10, 10);
            return product;
        }

        private static EventCollection<Product> GetDefaultEventCollection(Guid productId)
        {
            var priceChanges = ProductFactory.GetProductPriceChanges(productId).ToList();
            var stockMutationChanges = ProductFactory.GetProductStockMutations(productId).ToList();

            var changes = ChangeFactory.ToOrderedChangeEvent<Product, ProductPriceChange, double, DateTime>(
                x => x.Price,
                0,
                priceChanges,
                x => x.Price,
                x => x.ChangeDate).Cast<IEvent<Product>>().ToList();

            changes.AddRange(MutationFactory.ToOrderedMutationEvent<Product, ProductStockMutation, int, DateTimeOffset>(
                x => x.Available,
                stockMutationChanges,
                x => x.Amount,
                x => x.OrderDate).Cast<IEvent<Product>>());

            changes.AddRange(MutationFactory.ToOrderedMutationEvent<Product, ProductStockMutation, int, DateTimeOffset>(
                x => x.InStock,
                stockMutationChanges,
                x => x.Amount,
                x => x.DeliveryDate).Cast<IEvent<Product>>());

            var eventCollection = new EventCollection<Product>(changes);
            eventCollection.Count.Should().Be(changes.Count);

            return eventCollection;
        }

        [Theory]
        [Category("PropertyMutation")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Insert_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Contains(item).Should().BeFalse();

            eventCollection.Insert(index, item);

            product.Code.Should().Be("original");
            eventCollection.Contains(item).Should().BeTrue();
            eventCollection.IndexOf(item).Should().Be(index);
            eventCollection.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("PropertyMutation")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void InsertAndApply_Should_InsertAtIndex_With_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Contains(item).Should().BeFalse();

            eventCollection.InsertAndApply(index, item, product);

            product.Code.Should().Be("new");
            eventCollection.Contains(item).Should().BeTrue();
            eventCollection.IndexOf(item).Should().Be(index);
            eventCollection.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("PropertyMutation")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Insert(index, item);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(item).Should().BeTrue();

            eventCollection.RemoveAt(index);

            product.Code.Should().Be("original");
            eventCollection.Contains(item).Should().BeFalse();
            eventCollection.Count.Should().Be(count);
        }

        [Theory]
        [Category("PropertyMutation")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAndRevert_Should_RemoveAtIndex_With_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Insert(index, item);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(item).Should().BeTrue();

            eventCollection.RemoveAndRevert(index, product);

            product.Code.Should().Be("old");
            eventCollection.Contains(item).Should().BeFalse();
            eventCollection.Count.Should().Be(count);
        }

        [Theory]
        [Category("PropertyMutation")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveIfPresent_Without_EffectingItem(int index)
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Insert(index, item);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(item).Should().BeTrue();


            eventCollection.Remove(item).Should().BeTrue();
            eventCollection.Contains(item).Should().BeFalse();
            eventCollection.Remove(item).Should().BeFalse();

            product.Code.Should().Be("original");
            eventCollection.Count.Should().Be(count);
        }

        [Fact]
        [Category("PropertyMutation")]
        public void RemoveAndRevert_Should_RemoveIfPresent_With_EffectingItem()
        {
            var product = GetDefaultProduct();
            var eventCollection = GetDefaultEventCollection(product.Id);
            var count = eventCollection.Count;

            var item = new EventStub();
            eventCollection.Add(item);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(item).Should().BeTrue();

            eventCollection.RemoveAndRevert(item, product).Should().BeTrue();
            eventCollection.Contains(item).Should().BeFalse();
            eventCollection.RemoveAndRevert(item, product).Should().BeFalse();

            product.Code.Should().Be("old");
            eventCollection.Count.Should().Be(count);
        }

        private static void AssertProduct(Product product, double price, int available, int inStock)
        {
            product.Price.Should().BeApproximately(price, 0.000001);
            product.Available.Should().Be(available);
            product.InStock.Should().Be(inStock);
        }
    }
}
