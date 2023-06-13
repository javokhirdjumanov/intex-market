using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository.OrdersRepositories;
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
    public OrderDto MapToOrderDtoForFilters(filter_products_price_in_order_model filter_order)
    {
        string? weight = string.Empty;
        if (filter_order.weight != null)
            weight = $" x {filter_order.weight}";

        return new OrderDto(
            filter_order.order_id,
            filter_order.user_name,
            new FileDto(
                filter_order.file_id,
                filter_order.file_type,
                filter_order.file_name),
            filter_order.phone_number,
            $"{filter_order.height}{weight}/{filter_order.depth}",
            filter_order.price,
            new AddressDto(
                filter_order.address_id,
                filter_order.country,
                filter_order.city,
                filter_order.region,
                filter_order.street,
                filter_order.postal_code),
            filter_order.create_at);
    }

    public OrderReportDto MapToReportDto(report_model model)
    {
        return new OrderReportDto(
            id: model.Id,
            category_name: model.CategoryName,
            quentity: model.Quantity);
    }
}
