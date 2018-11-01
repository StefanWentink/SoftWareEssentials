namespace SWE.OData.Test.Data
{
    using System.Collections.Generic;

    internal class Order
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public virtual Relation Relation { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public Order(int key)
        {
            Id = key;
            OrderLines = new List<OrderLine>();
        }

        public Order(int key, int orderNumber)
            : this(key)
        {
            OrderNumber = orderNumber;
        }
    }
}