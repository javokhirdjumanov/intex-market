using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace IndexMarket.Infrastructure.Repository;
public class ProductShapeRepository 
    : BaseRepository<ProductShape, Guid>, IProductShapeRepository
{
    private readonly AppDbContext appDbContext;
    public ProductShapeRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
        this.appDbContext = appDbContext;
    }
}
