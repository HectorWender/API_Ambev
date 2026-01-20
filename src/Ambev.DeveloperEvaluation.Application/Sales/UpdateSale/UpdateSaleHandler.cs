using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
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
            // 1. Validação dos dados de entrada
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // 2. Buscar venda existente no banco
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            // 3. Atualizar dados do cabeçalho (Cliente, Filial, Data)
            sale.Update(command.SaleDate, command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);

            // 4. Preparar e substituir os itens
            // Convertemos os itens do command para entidades SaleItem
            // Nota: Ao instanciar SaleItem, as regras de desconto já devem ser aplicadas no construtor ou método set
            var newItems = new List<SaleItem>();
            foreach (var itemCmd in command.Items)
            {
                var item = new SaleItem(itemCmd.ProductId, itemCmd.ProductName, itemCmd.UnitPrice, itemCmd.Quantity);
                // Garante que o desconto foi calculado
                item.ApplyDiscountRules(); 
                newItems.Add(item);
            }

            // Substitui a lista antiga pela nova e recalcula o total da venda
            sale.ReplaceItems(newItems);

            // 5. Persistir no banco
            await _saleRepository.UpdateAsync(sale, cancellationToken);

            // 6. Retornar resultado mapeado
            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}