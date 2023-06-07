using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class ProductCreationValidatorForRectangel : AbstractValidator<ProductForCreationDtoRectangel>
{
    public ProductCreationValidatorForRectangel()
    {
        RuleFor(c => c)
            .NotNull()
            .WithMessage("Object not null !");

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("Price cannot be null !");

        RuleFor(c => c.Amount).NotEmpty()
            .WithMessage("Amount cannot be null !");

        RuleFor(c => c.Height).NotEmpty()
            .WithMessage("Height cannot be null !");

        RuleFor(c => c.Weight).NotEmpty()
            .WithMessage("Weight cannot be null !");

        RuleFor(c => c.Depth).NotEmpty()
            .WithMessage("Depth cannot be null !");

        RuleFor(c => c.File_Id)
            .NotEqual(default(Guid))
            .WithMessage("File Id cannot default Guid !");

        RuleFor(c => c.Category_Id)
            .NotEqual(default(Guid))
            .WithMessage("Category Id cannot default Guid !");

        RuleFor(c => c.Shape_Id)
            .NotEqual(default(Guid))
            .WithMessage("Shape Id cannot default Guid !");
    }
}
