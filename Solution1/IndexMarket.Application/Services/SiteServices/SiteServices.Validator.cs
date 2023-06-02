using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class SiteServices
{
    public void ValidationSiteId(Guid siteId)
    {
        if(siteId == default)
        {
            throw new ValidationException($"The given userId is invalid: {siteId}");
        }
    }

    public void ValidationStorageSite(Site storageSite, Guid siteId)
    {
        if (storageSite is null)
        {
            throw new NotFoundException($"Couldn't find user with given id: {siteId}");
        }
    }

    public void ValidateSiteForModificationDto(SiteModificationDto siteModificationDto)
    {
        var validationResult = new SiteForMoficationDtoValidator()
            .Validate(siteModificationDto);


        if (validationResult.IsValid)
        {
            return;
        }

        var errors = JsonSerializer
                .Serialize(validationResult.Errors.Select(error => new
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                    AttemptedValue = error.AttemptedValue
                }));

        throw new ValidationException(errors);
    }
}
