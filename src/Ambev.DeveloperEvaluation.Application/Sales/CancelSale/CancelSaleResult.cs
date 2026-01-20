using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid SaleId { get; set; }
        public string NewStatus { get; set; }
    }
}