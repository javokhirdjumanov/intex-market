namespace IndexMarket.Application.DataTransferObject;
public record SiteModificationDto(
    Guid siteId,
    string? PhoneNumber,
    string? JobTime,
    string? TelegramLink,
    string? InstagramLink,
    string? Country,
    string? City,
    string? Region,
    string? Street,
    short? PostalCode);
