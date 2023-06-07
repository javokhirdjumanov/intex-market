using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class SitesRepository : BaseRepository<Site, Guid>, ISitesRepository
{
    public SitesRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
