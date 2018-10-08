namespace SWE.EventSourcing.Test.Data
{
    using System;
    using System.Collections.Generic;

    internal static class ProductFactory
    {
        internal static Product GetProduct(string code)
        {
            return new Product(code, 1.0, 10, 10);
        }

        internal static List<ProductPriceChange> GetProductPriceChanges(Guid productId)
        {
            return new List<ProductPriceChange>
            {
                new ProductPriceChange(productId, 1.2, new DateTime(2018, 1, 1)),
                new ProductPriceChange(productId, 1.4, new DateTime(2018, 4, 1)),
                new ProductPriceChange(productId, 1.6, new DateTime(2018, 7, 1)),
                new ProductPriceChange(productId, 1.8, new DateTime(2018, 10, 1))
            };
        }

        internal static List<ProductStockMutation> GetProductStockMutations(Guid productId)
        {
            return new List<ProductStockMutation>
            {
                new ProductStockMutation(productId, 2, new DateTime(2018, 1, 10).ToLocalTime(), new DateTime(2018, 1, 20).ToLocalTime(), MutationType.Order),
                new ProductStockMutation(productId, 3, new DateTime(2018, 2, 10).ToLocalTime(), new DateTime(2018, 2, 20).ToLocalTime(), MutationType.Order),
                new ProductStockMutation(productId, 4, new DateTime(2018, 3, 10).ToLocalTime(), new DateTime(2018, 3, 20).ToLocalTime(), MutationType.Order),
                new ProductStockMutation(productId, 11, new DateTime(2018, 4, 10).ToLocalTime(), new DateTime(2018, 4, 20).ToLocalTime(), MutationType.Order)
            };
        }
    }
}
