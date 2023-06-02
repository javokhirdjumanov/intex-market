using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class CategoryRepository 
    : BaseRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
