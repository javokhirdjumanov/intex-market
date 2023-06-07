namespace IndexMarket.Application.DataTransferObject;
public record SiteDto(
    Guid siteId,
    string PhoneNumber,
    string JobTime,
    string TelegramLink,
    string InstagramLink,
    AddressDto Address);
