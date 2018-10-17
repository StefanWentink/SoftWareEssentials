namespace SWE.OData.Test.Data
{
    using System;
    using System.Collections.Generic;

    internal class Relation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Relation()
        {
            Id = Guid.NewGuid();
            Orders = new List<Order>();
        }

        public Relation(string name)
            : this()
        {
            Name = name;
        }
    }
}