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

    public IQueryable<filter_products_price_in_order_model> FilterOrdersByCreateAt_R(
        DateOnly from_date,
        DateOnly to_date)
        => this.appDbContext.FilterOrdersByCreatAt(from_date, to_date);

    public IQueryable<report_model> ReportOrdersWithQuantity(
        DateOnly start_date,
        DateOnly end_date,
        bool collect_quantity,
        long? quantity)
        => this.appDbContext.ReportOrdersWithQuantity(start_date, end_date, collect_quantity, quantity);
}
