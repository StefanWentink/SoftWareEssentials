namespace SWE.EventSourcing.Test.Data
{
    using SWE.Model.Interfaces;
    using System;

    public class Product : IKey
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public double Price { get; internal set; }

        public int Available { get; internal set; }

        public int InStock { get; internal set; }

        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Product(string code)
            : this(code, 0)
        {
        }

        public Product(string code, double price)
            : this(code, price, 0, 0)
        {
        }

        public Product(string code, int inStock, int available)
            : this(code, 0, inStock, available)
        {
        }

        public Product(string code, double price, int inStock, int available)
            : this()
        {
            Code = code;
            Price = price;
            InStock = inStock;
            Available = available;
        }
    }
}