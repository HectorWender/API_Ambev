using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Enums; // Para acessar SaleStatus se necessário

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    // Geralmente no Update permitimos alterar quase tudo, exceto talvez o Número da Venda
    public class UpdateSaleRequest
    {
        public Guid Id { get; set; } // Opcional aqui, pois vem na URL, mas útil
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsCancelled { get; set; } // Ou usar o Enum status
        
        public List<UpdateSaleItemRequest> Items { get; set; }
    }

    public class UpdateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}