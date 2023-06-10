namespace IndexMarket.Application.DataTransferObject;
public record UserForCreationDto(
    string FirstName,
    string? LastName,
    string PhoneNumber,
    string Email,
    string Password,
    string Country,
    string? City,
    string? Region,
    string? Street,
    short? PostalCode);
