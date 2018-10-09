namespace SWE.EventSourcing.Test.Factories
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.EventSourcing.Factories;
    using SWE.EventSourcing.Test.Data;
    using System.Collections.Generic;
    using System.Linq;
    using SWE.EventSourcing.Extensions;
    using System;
    using FluentAssertions;

    public class MutationFactoryTest
    {
        [Fact]
        [Category("MutationFactory")]
        public void ToMutationEvent_Should_ReturnMutationForEachItem()
        {
            var product = ProductFactory.GetProduct("0");
            product.Available.Should().Be(10);

            var product1 = ProductFactory.GetProduct("1");
            var product2 = ProductFactory.GetProduct("2");

            var mutations = new List<Product>
            {
                product1,
                product2
            };

            var actual = MutationFactory.ToMutationEvent<Product, Product, Guid, int>(
                x => x.Available,
                mutations,
                x => x.Id,
                x => x.InStock)
            .ToList();

            Assert.Equal(mutations.Count, actual.Count);
            var mutation1 = actual[0];
            var mutation2 = actual.Last();

            mutation1.Revert(product);
            product.Available.Should().Be(10 - product1.InStock);

            mutation2.Apply(product);
            product.Available.Should().Be(10 - product1.InStock + product2.InStock);
        }

        [Fact]
        [Category("MutationFactory")]
        public void ToOrderedMutationEvent_Should_ReturnMutationForEachItem()
        {
            var product = ProductFactory.GetProduct("0");
            Assert.Equal(10, product.Available);

            var mutations = ProductFactory.GetProductStockMutations(product.Id);

            var actual = MutationFactory.ToOrderedMutationEvent<Product, ProductStockMutation, string, int, DateTimeOffset>(
                x => x.Available,
                mutations,
                x => x.Id,
                x => x.Amount,
                x => x.OrderDate).ToList();

            Assert.Equal(mutations.Count, actual.Count);
            var mutation1 = actual[0];
            var mutation2 = actual.Last();

            mutation1.Revert(product);
            product.Available.Should().Be(10 - mutations[0].Amount);

            mutation2.Apply(product);
            product.Available.Should().Be(10 - mutations[0].Amount + mutations.Last().Amount);
        }
    }
}