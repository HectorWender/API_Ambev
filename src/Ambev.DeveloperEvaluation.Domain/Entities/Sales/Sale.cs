using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class Sale(string saleNumber, string customerName, string branchName) : BaseEntity
    {
        public string SaleNumber { get; set; } = saleNumber;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = customerName;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = branchName;
        public SaleStatus Status { get; set; } = SaleStatus.Pending; // Default status on create
        public decimal TotalAmount { get; set; }

        public ICollection<SaleItem> Items { get; set; } = (List<SaleItem>)[];

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

        public void Update(DateTime saleDate, Guid customerId, string customerName, Guid branchId, string branchName)
        {
            if (Status is SaleStatus.Cancelled or SaleStatus.Completed)
                throw new InvalidOperationException($"Cannot update a sale with status {Status}");

            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;

        }

        public void ReplaceItems(IEnumerable<SaleItem> newItems)
        {
            if (Status == SaleStatus.Cancelled)
                throw new InvalidOperationException("Cannot update items of a cancelled sale");

            // Remove all items
            Items.Clear();

            foreach (var item in newItems)
            {
                Items.Add(item);
            }

            CalculateTotal();
        }
    }
}