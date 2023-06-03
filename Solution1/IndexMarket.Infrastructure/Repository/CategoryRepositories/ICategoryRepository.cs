using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface ICategoryRepository : IBaseRepository<Category, Guid>
{
    ValueTask<Category> GetCategoryByNameAsync(string categoryName);
}
