using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Application.Services;
public partial class SiteServices : ISiteServices
{
    private readonly AppDbContext appDbContext;
    private readonly ISitesRepository sitesRepository;
    private readonly ISiteFactory siteFactory;
    public SiteServices(AppDbContext appDbContext, ISitesRepository sitesRepository, ISiteFactory siteFactory)
    {
        this.appDbContext = appDbContext;
        this.sitesRepository = sitesRepository;
        this.siteFactory = siteFactory;
    }
    public IQueryable<SiteDto> RetrieveSites()
    {
        var allSites = this.sitesRepository.SelectAll().Include(a => a.Address);

        return allSites.Select(sit => this.siteFactory.MapToSiteDto(sit));
    }
    public async ValueTask<SiteDto> ModifySiteAsync(SiteModificationDto siteModificationDto)
    {
        ValidateSiteForModificationDto(siteModificationDto);

        var storageSite = await this.sitesRepository.SelectByIdWithDetailsAsync(
            expression: site => site.Id == siteModificationDto.siteId,
            includes: new string[] { nameof(Site.Address) });

        ValidationStorageSite(storageSite, siteModificationDto.siteId);

        this.siteFactory.MatToSite(storageSite, siteModificationDto);

        var modifySite = await this.sitesRepository.UpdateAsync(storageSite);

        return this.siteFactory.MapToSiteDto(modifySite);
    }
}
