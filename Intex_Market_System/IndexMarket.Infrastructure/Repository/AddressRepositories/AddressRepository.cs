using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
{
    public AddressRepository(AppDbContext appDbContext) 
        : base(appDbContext) { }
}
