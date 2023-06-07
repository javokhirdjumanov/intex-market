namespace IndexMarket.Application.DataTransferObject;
public record AddressDto(
    Guid AddressId,
    string Country,
    string? City,
    string? Region,
    string? Street,
    short? PostalCode);
