using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            // Executa a lógica de domínio (Regra de Negócio)
            sale.Cancel();

            // Persiste a mudança no banco
            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return new CancelSaleResult
            {
                Success = true,
                SaleId = sale.Id,
                NewStatus = sale.Status.ToString(),
                Message = "Sale cancelled successfully"
            };
        }
    }
}