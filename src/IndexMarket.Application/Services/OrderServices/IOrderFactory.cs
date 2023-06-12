using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository.OrdersRepositories;

namespace IndexMarket.Application.Services;
public interface IOrderFactory
{
    OrderDto MapToOrderDto(Order order);
    OrderDto MapToOrderDtoForFilters(filter_products_price_in_order_model model);
    OrderReportDto MapToReportDto(report_model model);
}
