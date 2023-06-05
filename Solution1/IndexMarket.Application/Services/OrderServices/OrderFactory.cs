using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public class OrderFactory : IOrderFactory
{
    public OrderDto MapToOrderDto(Order newOrder)
    {
        string? weight = string.Empty;
        if (newOrder.Product.Weight != null)
            weight = $" x {newOrder.Product.Weight}";

        return new OrderDto(
            newOrder.User.FirstName,
            newOrder.User.PhoneNumber,
            newOrder.Product.PhotoLink,
            $"{newOrder.Product.Height}{weight}/{newOrder.Product.Depth}",
            newOrder.Product.Price,
            new AddressDto(
                newOrder.User.Address!.Country,
                newOrder.User.Address.City,
                newOrder.User.Address.Region,
                newOrder.User.Address.Street,
                newOrder.User.Address.PostalCode),
            newOrder.CreatedAt);
    }
}
