using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class OrderRepository 
    : BaseRepository<Order, Guid>, IOrderRepository
{
    public OrderRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
