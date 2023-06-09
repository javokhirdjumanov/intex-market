using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository.OrdersRepositories;

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

    public IQueryable<filter_products_price_in_order_model> FilterOrdersByProductPrice_R(
        decimal? from_price,
        decimal? to_price)
        => this.appDbContext.FilterOrdersByProductPrice(from_price, to_price);
}
