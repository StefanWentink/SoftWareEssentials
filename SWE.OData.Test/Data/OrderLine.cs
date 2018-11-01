namespace SWE.OData.Test.Data
{
    using System;

    internal class OrderLine
    {
        public Guid Id { get; set; }

        public string ProductNumber { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public virtual Order Order { get; set; }

        public OrderLine()
        {
            Id = Guid.NewGuid();
        }

        public OrderLine(string productNumber, int amount, double price)
            : this()
        {
            ProductNumber = productNumber;
            Amount = amount;
            Price = price;
        }
    }
}