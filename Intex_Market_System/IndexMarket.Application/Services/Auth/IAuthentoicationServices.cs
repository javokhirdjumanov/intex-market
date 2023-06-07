using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IAuthentoicationServices
{
    ValueTask<TokenDto> LoginAsync(AuthentificationDto authentificationDto);
    ValueTask<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
}
