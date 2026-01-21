using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler(ISaleRepository saleRepository)
        : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
            
            sale.Cancel();
            await saleRepository.UpdateAsync(sale, cancellationToken);
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