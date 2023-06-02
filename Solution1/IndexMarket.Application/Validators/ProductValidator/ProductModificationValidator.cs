using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validator;
public class ProductModificationValidator : AbstractValidator<ProductForModificationDto>
{
    public ProductModificationValidator()
    {
        
    }
}
