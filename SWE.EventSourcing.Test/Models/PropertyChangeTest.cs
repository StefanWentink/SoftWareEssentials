namespace SWE.EventSourcing.Test.Models
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.EventSourcing.Models;
    using SWE.EventSourcing.Test.Data;
    using FluentAssertions;

    public class PropertyChangeTest
    {
        [Fact]
        [Category("PropertyChange")]
        public void PropertyChange_Constructor_Should_SetCorrectProperties()
        {
            const string previousValue = "1";
            const string value = "2";

            var propertyChange = new PropertyChange<Product, string>(previousValue, value, x => x.Code);
            propertyChange.PreviousValue.Should().Be(previousValue);
            propertyChange.Value.Should().Be(value);
        }

        [Fact]
        [Category("PropertyChange")]
        public void PropertyChange_Should_HoldValidActions_WhenInt()
        {
            var propertyChange = new PropertyChange<Product, int>(2, 4, x => x.Available);
            var product = new Product("test");
            Assert.Equal(0, product.Available);
            product.Available.Should().Be(0);

            propertyChange.GetRevertValueAction()(product);
            product.Available.Should().Be(propertyChange.PreviousValue);

            propertyChange.GetApplyValueAction()(product);
            product.Available.Should().Be(propertyChange.Value);
        }

        [Fact]
        [Category("PropertyChange")]
        public void PropertyChange_Should_HoldValidActions_WhenDouble()
        {
            var propertyChange = new PropertyChange<Product, double>(4.2, 2.1, x => x.Price);
            var product = new Product("test");
            product.Price.Should().Be(0);

            propertyChange.GetRevertValueAction()(product);
            product.Price.Should().Be(propertyChange.PreviousValue);

            propertyChange.GetApplyValueAction()(product);
            product.Price.Should().Be(propertyChange.Value);
        }
    }
}