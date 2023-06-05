using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class OrderCreationValidator : AbstractValidator<OrderCreationDto>
{
    public OrderCreationValidator()
    {
        RuleFor(o => o)
            .NotNull().WithMessage("Object cannot null !");

        RuleFor(o => o.Product_Id)
            .NotEqual(default(Guid))
            .WithMessage("Product Id cannot default guid !");

        RuleFor(o => o.User_Id)
            .NotEqual(default(Guid))
            .WithMessage("User Id cannot default guid !");

        RuleFor(o => o.Address_Id)
            .NotEqual(default(Guid))
            .WithMessage("Address Id cannot default guid !");
    }
}
