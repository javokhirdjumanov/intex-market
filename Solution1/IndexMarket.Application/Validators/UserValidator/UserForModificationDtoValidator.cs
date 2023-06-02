using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class UserForModificationDtoValidator : AbstractValidator<UserForModificationDto>
{
    public UserForModificationDtoValidator()
    {
        RuleFor(u => u)
            .NotNull();

        RuleFor(user => user.userId)
            .NotEqual(default(Guid));
    }
}
