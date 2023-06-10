using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IOrderServices
{
    ValueTask<OrderDto> CreateOrderAsync(OrderCreationDto orderCreationDto);
    IEnumerable<OrderDto> GetAllOrders();
    ValueTask<OrderDto> GetOrderByIdAsync(Guid orderId);
    IQueryable<OrderDto> FilterOrdersByProductPrice_S(decimal? from_price, decimal? to_price);
    IQueryable<OrderDto> FilterOrdersByCreateAt(DateOnly from_date, DateOnly to_date);
    IEnumerable<OrderDto> SearchOrders(string columnName);
    ValueTask<OrderDto> DeleteOrdersAsync(Guid orderId);
}
