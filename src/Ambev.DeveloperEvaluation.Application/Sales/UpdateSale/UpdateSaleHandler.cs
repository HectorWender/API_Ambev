using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");
            
            sale.Update(command.SaleDate, command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);
            
            var newItems = new List<SaleItem>();
            foreach (var itemCmd in command.Items)
            {
                var item = new SaleItem(itemCmd.ProductId, itemCmd.ProductName, itemCmd.UnitPrice, itemCmd.Quantity);
                item.ApplyDiscountRules(); 
                newItems.Add(item);
            }

            // Replace the old list with the new one and recalculate the total sale
            sale.ReplaceItems(newItems);
            await _saleRepository.UpdateAsync(sale, cancellationToken);
            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}