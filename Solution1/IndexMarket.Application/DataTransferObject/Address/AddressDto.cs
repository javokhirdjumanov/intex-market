namespace IndexMarket.Application.DataTransferObject;
public record AddressDto(
    string Country,
    string? City,
    string? Region,
    string? Street,
    short? PostalCode);
