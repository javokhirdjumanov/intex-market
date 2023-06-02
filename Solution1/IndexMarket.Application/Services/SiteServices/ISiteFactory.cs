using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public interface ISiteFactory
{
    SiteDto MapToSiteDto(Site site);
    void MatToSite(Site storageSite, SiteModificationDto modificationDto);
}
