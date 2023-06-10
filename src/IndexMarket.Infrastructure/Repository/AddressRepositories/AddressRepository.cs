using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
{
    private readonly AppDbContext _appDbContext;
    public AddressRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public IQueryable<get_address_model> GetAllAddress()
        => _appDbContext.GetAllAddress();
}
