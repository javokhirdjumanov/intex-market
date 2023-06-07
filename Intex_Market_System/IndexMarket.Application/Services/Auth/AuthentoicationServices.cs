using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Exceptions;
using IndexMarket.Infrastructure.Auth;
using IndexMarket.Infrastructure.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IndexMarket.Application.Services;
public class AuthentoicationServices : IAuthentoicationServices
{
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenHandler jwtTokenHandler;
    private readonly IPasswordHasher passwordHasher;
    private readonly JwtOptions jwtOptions;
    public AuthentoicationServices(
        IUserRepository userRepository,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHasher passwordHasher,
        IOptions<JwtOptions> options)
    {
        this.userRepository = userRepository;
        this.jwtTokenHandler = jwtTokenHandler;
        this.passwordHasher = passwordHasher;
        this.jwtOptions = options.Value;
    }

    public async ValueTask<TokenDto> LoginAsync(AuthentificationDto authentificationDto)
    {
        var user = await this.userRepository
            .SelectByIdWithDetailsAsync(
            expression: user => user.Email == authentificationDto.email,
            includes: Array.Empty<string>());

        if(user is null)
        {
            throw new NotFoundException("User with given credentials not found.!");
        }

        if (!this.passwordHasher.Verify(
            hash: user.PasswordHash,
            password: authentificationDto.password,
            salt: user.Salt))
        {
            throw new ValidationException("Username or password is not Valid.!");
        }

        string refreshToken = this.jwtTokenHandler.GenerateRefreshToken();

        user.UpdateRefreshToken(refreshToken);

        var updateUser = await this.userRepository
            .UpdateAsync(user);

        var accessToken = this.jwtTokenHandler.GenerateAccessToken(updateUser);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
            refreshToken: user.RefreshToken,
            expiredDate: accessToken.ValidTo);
    }

    public async ValueTask<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var claimsPrincipal = GetPrincipalFromExpiredToken(
            refreshTokenDto.accessToken);

        var userId = claimsPrincipal.FindFirstValue(ClaimNames.Id);

        var storageUser = await this.userRepository
            .SelectByIdAsync(Guid.Parse(userId));

        if (!storageUser.RefreshToken.Equals(refreshTokenDto.refreshToken))
        {
            throw new ValidationException("Refresh token is not valid");
        }

        if (storageUser.RefreshTokenExpireDate <= DateTime.UtcNow)
        {
            throw new ValidationException("Refresh token has already been expired");
        }

        var newAccessToken = this.jwtTokenHandler
            .GenerateAccessToken(storageUser);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken: storageUser.RefreshToken,
            expiredDate: newAccessToken.ValidTo);
    }
    private ClaimsPrincipal GetPrincipalFromExpiredToken(
        string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = this.jwtOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = this.jwtOptions.Issuer,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.jwtOptions.SecretKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(
            token: accessToken,
            validationParameters: tokenValidationParameters,
            validatedToken: out SecurityToken securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(
            SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ValidationException("Invalid token");
        }

        return principal;
    }
}
