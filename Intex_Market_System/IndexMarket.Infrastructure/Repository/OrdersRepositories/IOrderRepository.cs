using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository.OrdersRepositories;

namespace IndexMarket.Infrastructure.Repository;
public interface IOrderRepository
    : IBaseRepository<Order, Guid>
{
    IQueryable<filter_products_price_in_order_model> FilterOrdersByProductPrice_R(
        decimal? from_price,
        decimal? to_price);

    IQueryable<filter_products_price_in_order_model> FilterOrdersByCreateAt_R(
        DateOnly from_date,
        DateOnly to_date);
}
