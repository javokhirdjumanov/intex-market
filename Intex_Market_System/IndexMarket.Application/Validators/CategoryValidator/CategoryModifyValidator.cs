using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class CategoryModifyValidator : AbstractValidator<CategoryModifyDto>
{
    public CategoryModifyValidator()
    {
        RuleFor(c => c)
            .NotNull();

        RuleFor(c => c.id)
            .NotEqual(default(Guid));

        RuleFor(c => c.Title)
            .NotEmpty();
    }
}
