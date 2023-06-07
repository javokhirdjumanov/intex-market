using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class ProductRepository
    : BaseRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
