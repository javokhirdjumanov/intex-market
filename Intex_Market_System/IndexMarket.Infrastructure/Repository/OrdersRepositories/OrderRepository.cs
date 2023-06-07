using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Infrastructure.Repository;
public class OrderRepository 
    : BaseRepository<Order, Guid>, IOrderRepository
{
    private readonly AppDbContext appDbContext;
    public OrderRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    {
        this.appDbContext = appDbContext;
    }
}
