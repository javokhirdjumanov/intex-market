using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Infrastructure.Repository;
public class CategoryRepository 
    : BaseRepository<Category, Guid>, ICategoryRepository
{
    private readonly AppDbContext appDbContext;
    public CategoryRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<Category> GetCategoryByNameAsync(string categoryName)
    {
        var category = await this.appDbContext
            .Set<Category>()
            .FirstOrDefaultAsync(x => x.Title == categoryName);

        return category;
    }
}
