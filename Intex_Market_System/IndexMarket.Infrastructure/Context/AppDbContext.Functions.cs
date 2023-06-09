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
    public IQueryable<filter_products_price_in_order_model>
        Filter_Orders_By_Product_Price(decimal? from_price, decimal? to_price)
        => FromExpression(() => Filter_Orders_By_Product_Price(from_price, to_price));
}
