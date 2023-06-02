namespace IndexMarket.Application.DataTransferObject;
public record TokenDto(string accessToken, string? refreshToken, DateTime expiredDate);