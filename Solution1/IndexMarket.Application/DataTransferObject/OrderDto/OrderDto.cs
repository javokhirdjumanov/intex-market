namespace IndexMarket.Application.DataTransferObject;
public record OrderDto(
    string UserName,
    string PhoneNumber,
    string? PhotoLink,
    string SizeProduct,
    decimal Price,
    AddressDto Address,
    DateTime CreateAt);
