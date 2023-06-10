using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IAddressRepository : IBaseRepository<Address, Guid>
{
    IQueryable<get_address_model> GetAllAddress();
}
