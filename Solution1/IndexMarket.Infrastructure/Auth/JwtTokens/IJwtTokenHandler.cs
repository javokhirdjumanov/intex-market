using IndexMarket.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace IndexMarket.Infrastructure.Auth;
public interface IJwtTokenHandler
{
    JwtSecurityToken GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
