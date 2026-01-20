using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required");
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.Items).NotEmpty().WithMessage("Sale must have at least one item");
            
            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemValidator());
        }
    }

    public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(20)
                .WithMessage("Quantity must be between 1 and 20.");
            
            RuleFor(x => x.UnitPrice).GreaterThan(0);
        }
    }
}