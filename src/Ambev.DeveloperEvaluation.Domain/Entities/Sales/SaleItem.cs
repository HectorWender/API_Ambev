using System;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }

        public void ApplyDiscountRules()
        {
            if (Quantity > 20) throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            if (Quantity < 4) Discount = 0;
            else if (Quantity is >= 4 and < 10) Discount = (UnitPrice * Quantity) * 0.10m; // discount 10%
            else if (Quantity is >= 10 and <= 20) Discount = (UnitPrice * Quantity) * 0.20m; // 20%

            TotalAmount = (UnitPrice * Quantity) - Discount;
        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}