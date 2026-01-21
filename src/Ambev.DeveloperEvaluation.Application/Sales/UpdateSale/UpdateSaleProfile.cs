using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            // Entity Mapping -> Result
            CreateMap<Sale, UpdateSaleResult>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<SaleItem, UpdateSaleItemResult>();

            // Command Mapping -> Entity Item (for list recreation)
            CreateMap<UpdateSaleItemCommand, SaleItem>()
                .ConstructUsing(c => new SaleItem(c.ProductId, c.ProductName, c.UnitPrice, c.Quantity));
        }
    }
}