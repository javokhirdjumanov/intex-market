using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;

public class UserForCreationDtoValidator : AbstractValidator<UserForCreationDto>
{
    public UserForCreationDtoValidator()
    {
        RuleFor(user => user)
            .NotNull();

        RuleFor(user => user.FirstName)
            .MaximumLength(100).WithMessage("Length should not exceed 100")
            .NotEmpty().WithMessage("Not Empty !");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage("Not Empty !");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Not Empty !");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Not Empty !");

        RuleFor(user => user.Country)
            .NotEmpty().WithMessage("Not Empty !");
    }
}
