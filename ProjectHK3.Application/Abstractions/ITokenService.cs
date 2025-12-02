using System.Security.Claims;

namespace ProjectHK3.Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(int Id, string Email, string Role = "Employee", IEnumerable<Claim>? extraClaims = null);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
