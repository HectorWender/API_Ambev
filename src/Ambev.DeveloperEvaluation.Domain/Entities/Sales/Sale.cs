using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } 
        public SaleStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

        public Sale()
        {
            Status = SaleStatus.Pending; // Default status on create
        }
        
        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.TotalAmount);
        }

        public void Cancel()
        {
            if (Status == SaleStatus.Completed)
                throw new DomainException("Completed sales cannot be cancelled.");
            
            Status = SaleStatus.Cancelled;
            
            foreach (var item in Items)
            {
                item.Cancel();
            }
        }
    }
}