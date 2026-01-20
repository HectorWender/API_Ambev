using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Request
{
    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}