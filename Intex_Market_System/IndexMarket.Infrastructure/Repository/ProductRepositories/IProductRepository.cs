using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IProductRepository : IBaseRepository<Product, Guid>
{

}
