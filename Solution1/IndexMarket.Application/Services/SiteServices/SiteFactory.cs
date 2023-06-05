using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public class SiteFactory : ISiteFactory
{
    public SiteDto MapToSiteDto(Site site)
    {
        AddressDto? addressDto = default;

        if (site.Address is not null)
        {
            addressDto = new AddressDto(
                AddressId: site.Address.Id,
                Country: site.Address.Country,
                City: site.Address.City,
                Region: site.Address.Region,
                Street: site.Address.Street,
                PostalCode: site.Address.PostalCode);
        }

        return new SiteDto(
            siteId: site.Id,
            PhoneNumber: site.PhoneNumber,
            JobTime: site.JobTime,
            TelegramLink: site.TelegrammLink,
            InstagramLink: site.InstagramLink,
            Address: addressDto);
    }

    public void MatToSite(Site storageSite, SiteModificationDto modificationDto)
    {
        storageSite.PhoneNumber = modificationDto.PhoneNumber ?? storageSite.PhoneNumber;
        storageSite.JobTime = modificationDto.JobTime ?? storageSite.JobTime;
        storageSite.TelegrammLink = modificationDto.TelegramLink ?? storageSite.TelegrammLink;
        storageSite.InstagramLink = modificationDto.InstagramLink ?? storageSite.InstagramLink;
        storageSite.UpdatedAt = DateTime.UtcNow;

        storageSite.Address = storageSite.Address ?? new Address();
        storageSite.Address.Country = modificationDto?.Country ?? storageSite.Address.Country;
        storageSite.Address.City = modificationDto?.City ?? storageSite.Address.City;
        storageSite.Address.Region = modificationDto?.Region ?? storageSite.Address.Region;
        storageSite.Address.Street = modificationDto?.Street ?? storageSite.Address.Street;
        storageSite.Address.PostalCode = modificationDto?.PostalCode ?? storageSite.Address.PostalCode;
    }
}
