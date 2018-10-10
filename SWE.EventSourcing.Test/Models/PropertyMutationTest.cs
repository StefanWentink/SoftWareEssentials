namespace SWE.EventSourcing.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.EventSourcing.Models;
    using SWE.EventSourcing.Test.Data;
    using SWE.Xunit.Attributes;
    using System;

    public class PropertyMutationTest
    {
        [Fact]
        [Category("PropertyMutation")]
        public void PropertyMutation_Constructor_Should_SetCorrectProperties()
        {
            const double value = 2;

            var propertyMutation = new PropertyMutation<Product, double>(value, x => x.Price);
            propertyMutation.Value.Should().Be(value);
        }

        [Fact]
        [Category("PropertyMutation")]
        public void PropertyMutation_Constructor_Should_ThrowException_When_TypeNotInvertable()
        {
            const string value = "2";

            Assert.Throws<ArgumentException>(() => new PropertyMutation<Product, string>(value, x => x.Code));
        }

        [Fact]
        [Category("PropertyMutation")]
        public void PropertyMutation_Should_HoldValidActions_WhenInt()
        {
            const int value = 2;
            const int incrementValue = 3;

            var propertyMutation = new PropertyMutation<Product, int>(incrementValue, x => x.Available);
            var product = new Product("test", value, value);
            product.Available.Should().Be(value);

            propertyMutation.GetApplyValueAction()(product);
            product.Available.Should().Be(value + incrementValue);

            propertyMutation.GetApplyValueAction()(product);
            product.Available.Should().Be(value + incrementValue + incrementValue);

            propertyMutation.GetRevertValueAction()(product);
            product.Available.Should().Be(value + incrementValue);
        }

        [Fact]
        [Category("PropertyMutation")]
        public void PropertyMutation_Should_HoldValidActions_WhenDouble()
        {
            const double value = 2.3;
            const double incrementValue = 3.2;

            var propertyMutation = new PropertyMutation<Product, double>(incrementValue, x => x.Price);
            var product = new Product("test", value);
            product.Price.Should().BeApproximately(value, 0.000001);

            propertyMutation.GetApplyValueAction()(product);
            product.Price.Should().BeApproximately(value + incrementValue, 0.000001);

            propertyMutation.GetApplyValueAction()(product);
            product.Price.Should().BeApproximately(value + incrementValue + incrementValue, 0.000001);

            propertyMutation.GetRevertValueAction()(product);
            product.Price.Should().BeApproximately(value + incrementValue, 0.000001);
        }
    }
}