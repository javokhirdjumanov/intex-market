using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace IndexMarket.Infrastructure.Repository;
public class ProductShapeRepository 
    : BaseRepository<ProductShape, Guid>, IProductShapeRepository
{
    public ProductShapeRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
