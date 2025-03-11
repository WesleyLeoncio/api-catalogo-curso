using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api_catalogo_curso.modules.token.service.interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config);
    
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config);
}