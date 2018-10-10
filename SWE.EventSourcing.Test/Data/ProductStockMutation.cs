using System;

namespace SWE.EventSourcing.Test.Data
{
    internal class ProductStockMutation
    {
        public string Id { get; }

        public Guid ProductId { get; }

        public int Amount { get; }

        public int ApplyableAmount =>
            MutationType == MutationType.Supply
            ? Amount
            : Amount * -1;

        public DateTimeOffset OrderDate { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public MutationType MutationType { get; set; }

        public ProductStockMutation()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ProductStockMutation(
            Guid productId,
            int amount,
            DateTimeOffset orderDate,
            DateTimeOffset deliveryDate,
            MutationType mutationType)
        {
            ProductId = productId;
            Amount = amount;
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            MutationType = mutationType;
        }
    }
}