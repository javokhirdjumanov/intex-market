using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository;
using IndexMarket.Infrastructure.Repository.OrdersRepositories;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Infrastructure.Context;
public partial class AppDbContext
{
    [DbFunction("get_adresses", Schema = "public")]
    public IQueryable<get_address_model> GetAllAddress()
        => FromExpression(() => GetAllAddress());

    [DbFunction("filter_products_price_in_order", Schema = "public")]
    public IQueryable<filter_products_price_in_order_model> FilterOrdersByProductPrice(
        decimal? from_price,
        decimal? to_price) 
        => FromExpression(() => FilterOrdersByProductPrice(from_price, to_price));
}
