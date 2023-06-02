namespace IndexMarket.Application.DataTransferObject;
public record UserForModificationDto(
    Guid userId,
    string? firstName,
    string? lastName,
    string? phoneNumber,
    string? country,
    string? city,
    string? region,
    string? street,
    short? postalCode
    );
