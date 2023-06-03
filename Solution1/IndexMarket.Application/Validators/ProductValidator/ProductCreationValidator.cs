using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class ProductCreationValidator : AbstractValidator<ProductForCreationDto>
{
    public ProductCreationValidator()
    {
        RuleFor(p => p.Price)
            .NotEqual(null)
            .WithMessage("Price cannot null !");

        RuleFor(p => p.Amount)
            .NotEqual(null)
            .WithMessage("Amount cannot null !");

        RuleFor(p => p.Height)
            .NotEqual(null)
            .WithMessage("Height cannot null !");

        RuleFor(p => p.Depth)
            .NotEqual(null)
            .WithMessage("Depth cannot null !");

        RuleFor(p => p.Category_Id)
            .NotEqual(default(Guid))
            .WithMessage("Category cannot default Guid !");

        RuleFor(p => p.Shape)
            .NotEmpty()
            .WithMessage("ProductShape cannot empty !");
    }
}
