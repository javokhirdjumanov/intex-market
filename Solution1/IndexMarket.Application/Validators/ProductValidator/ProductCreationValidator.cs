using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class ProductCreationValidator : AbstractValidator<ProductForCreationDto>
{
    public ProductCreationValidator()
    {
        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("Price cannot null !");

        RuleFor(p => p.Amount)
            .NotEmpty()
            .WithMessage("Amount cannot null !");

        RuleFor(p => p.Height).NotEmpty()
            .WithMessage("Height cannot null !");

        RuleFor(p => p.Depth).NotEmpty()
            .WithMessage("Depth cannot null !");

        RuleFor(p => p.Category_Id)
            .NotEqual(default(Guid))
            .WithMessage("Category cannot default Guid !");

        RuleFor(p => p.Shape_Id)
            .NotEqual(default(Guid))
            .WithMessage("Shape cannot default Guid !");
    }
}
