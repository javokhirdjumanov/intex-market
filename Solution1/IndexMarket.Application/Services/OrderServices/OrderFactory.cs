using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace IndexMarket.Application.Services;
public class OrderFactory : IOrderFactory
{
    public OrderDto MapToOrderDto(Order newOrder)
    {
        string? weight = string.Empty;
        if (newOrder.Product.Weight != null)
            weight = $" x {newOrder.Product.Weight}";

        return new OrderDto(
            newOrder.Id,
            newOrder.User.FirstName,
            new FileDto(
                newOrder.Product.File.Id,
                newOrder.Product.File.Type,
                newOrder.Product.File.FileName),
            newOrder.User.PhoneNumber,
            $"{newOrder.Product.Height}{weight}/{newOrder.Product.Depth}",
            newOrder.Product.Price,
            new AddressDto(
                newOrder.User.Address.Id,
                newOrder.User.Address!.Country,
                newOrder.User.Address.City,
                newOrder.User.Address.Region,
                newOrder.User.Address.Street,
                newOrder.User.Address.PostalCode),
            newOrder.CreatedAt);
    }
}
