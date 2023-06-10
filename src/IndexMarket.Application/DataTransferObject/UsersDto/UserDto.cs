using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.DataTransferObject;
public record UserDto(
    Guid id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    string Role,
    AddressDto? address);
