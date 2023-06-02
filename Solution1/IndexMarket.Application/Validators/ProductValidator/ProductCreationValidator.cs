using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class ProductCreationValidator : AbstractValidator<ProductForCreationDto>
{
    public ProductCreationValidator()
    {
        RuleFor(p => p)
            .NotNull();

        RuleFor(p => p.Price)
            .NotEqual(default(decimal)).WithMessage("Price cannot be null !");

        RuleFor(p => p.Amount)
            .NotEqual(default(int)).WithMessage("Amount cannot be null !");

        RuleFor(p => p.Frame)
            .NotEmpty().WithMessage("Frame cannot be empty !")
            .MaximumLength(100).WithMessage("Length should not exceed 100 !");

        RuleFor(p => p.Height)
            .NotEqual(default(double)).WithMessage("Height cannot be null !");
    }
}
