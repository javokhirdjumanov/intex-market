using FluentValidation;
using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Validators;
public class SiteForMoficationDtoValidator : AbstractValidator<SiteModificationDto>
{
    public SiteForMoficationDtoValidator()
    {
        RuleFor(s => s)
            .NotNull();

        RuleFor(s => s.siteId)
            .NotEqual(default(Guid));
    }
}
