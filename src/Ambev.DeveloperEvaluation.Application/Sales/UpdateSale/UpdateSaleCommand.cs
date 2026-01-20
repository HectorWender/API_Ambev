using System;
using System.Collections.Generic;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; } // O ID da venda que será atualizada
        public string SaleNumber { get; set; } // Geralmente não muda, mas pode vir no request
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; }
    }

    public class UpdateSaleItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}