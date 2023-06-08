using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

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

    public ValueTask<Order> FilterOrders(string columnName)
    {
        throw new NotImplementedException();
    }
}
