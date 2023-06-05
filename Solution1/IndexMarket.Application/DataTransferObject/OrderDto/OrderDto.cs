namespace IndexMarket.Application.DataTransferObject;
public record OrderDto(
    Guid? orderId,
    string UserName,
    string PhoneNumber,
    string? PhotoLink,
    string SizeProduct,
    decimal Price,
    AddressDto Address,
    DateTime CreateAt);
