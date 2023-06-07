using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface ISiteServices
{
    IQueryable<SiteDto> RetrieveSites();
    ValueTask<SiteDto> ModifySiteAsync(SiteModificationDto siteModificationDto);
}
