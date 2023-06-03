using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class ProductCreationValidator : AbstractValidator<ProductForCreationDto>
{
    public ProductCreationValidator()
    {
        RuleFor(p => p.Price)
            .NotEqual(default(decimal));

        RuleFor(p => p.Amount)
            .NotEqual(default(int));

        RuleFor(p => p.Height)
            .NotEqual(default(double));

        RuleFor(p => p.Depth)
            .NotEqual(default(int));

        RuleFor(p => p.Category_Id)
            .NotEqual(default(Guid));

        RuleFor(p => p.Shape)
            .NotEmpty()
            .WithMessage("ProductShape cannot empty !");
    }
}
