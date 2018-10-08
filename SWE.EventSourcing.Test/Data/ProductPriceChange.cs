﻿namespace SWE.EventSourcing.Test.Data
{
    using SWE.Model.Interfaces;
    using System;

    internal class ProductPriceChange : IKey
    {
        public Guid Id { get; }

        public Guid ProductId { get; }

        public double Price { get; }

        public DateTime ChangeDate { get; set; }

        public ProductPriceChange()
        {
            Id = Guid.NewGuid();
        }

        public ProductPriceChange(
            Guid productId,
            double price,
            DateTime changeDate)
        {
            ProductId = productId;
            Price = price;
            ChangeDate = changeDate;
        }
    }
}