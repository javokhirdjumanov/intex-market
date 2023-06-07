using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public interface IOrderFactory
{
    OrderDto MapToOrderDto(Order order);
}
