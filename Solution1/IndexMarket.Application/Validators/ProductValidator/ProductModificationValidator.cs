using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validator;
public class ProductModificationValidator : AbstractValidator<ProductForModificationDto>
{
    public ProductModificationValidator()
    {
        RuleFor(p => p)
            .NotNull().WithMessage("Product cannot null !");

        RuleFor(p => p.Product_Id)
            .NotEqual(default(Guid));
    }
}
