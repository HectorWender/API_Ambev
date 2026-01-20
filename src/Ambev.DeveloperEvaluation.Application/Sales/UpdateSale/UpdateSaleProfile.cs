using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            // Mapeamento de Entidade -> Resultado
            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<SaleItem, UpdateSaleItemResult>();

            // Mapeamento de Comando -> Item de Entidade (para recriação da lista)
            CreateMap<UpdateSaleItemCommand, SaleItem>()
                .ConstructUsing(c => new SaleItem(c.ProductId, c.ProductName, c.UnitPrice, c.Quantity));
        }
    }
}