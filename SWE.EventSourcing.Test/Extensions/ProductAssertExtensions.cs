namespace SWE.EventSourcing.Test.Extensions
{
    using FluentAssertions;
    using SWE.EventSourcing.Test.Data;

    internal static class ProductAssertExtensions
    {
        internal static void AssertProduct(this Product product, double price, int available, int inStock)
        {
            product.Price.Should().BeApproximately(price, 0.000001);
            product.Available.Should().Be(available);
            product.InStock.Should().Be(inStock);
        }
    }
}
