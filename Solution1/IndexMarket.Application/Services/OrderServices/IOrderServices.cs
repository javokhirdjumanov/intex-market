using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IOrderServices
{
    ValueTask<OrderDto> CreateOrderAsync(OrderCreationDto orderCreationDto);
    IEnumerable<OrderDto> GetAllOrders();
    ValueTask<OrderDto> GetOrderByIdAsync(Guid orderId);
    ValueTask<OrderDto> DeleteOrdersAsync(Guid orderId);
}
