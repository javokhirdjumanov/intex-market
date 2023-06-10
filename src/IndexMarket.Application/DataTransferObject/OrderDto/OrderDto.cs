namespace IndexMarket.Application.DataTransferObject;
public record OrderDto(
    Guid? orderId,
    string UserName,
    FileDto file,
    string PhoneNumber,
    string SizeProduct,
    decimal Price,
    AddressDto Address,
    DateTime CreateAt);
