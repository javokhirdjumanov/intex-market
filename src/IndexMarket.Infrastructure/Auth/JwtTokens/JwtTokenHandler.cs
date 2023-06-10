using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IndexMarket.Infrastructure.Auth;
public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly JwtOptions jwtOptions;
    public JwtTokenHandler(IOptions<JwtOptions> options)
    {
        this.jwtOptions = options.Value;
    }

    public JwtSecurityToken GenerateAccessToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimNames.Id, user.Id.ToString()),
            new Claim(ClaimNames.Email, user.Email),
            new Claim(ClaimNames.Role, Enum.GetName<UserRoles>(user.Role))
        };

        var authSingingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtOptions.SecretKey));

        var token = new JwtSecurityToken(
            issuer: this.jwtOptions.Issuer,
            audience: this.jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(this.jwtOptions.ExpirationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSingingKey,
                algorithm: SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];

        using var randomGenerator = RandomNumberGenerator.Create();

        randomGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
