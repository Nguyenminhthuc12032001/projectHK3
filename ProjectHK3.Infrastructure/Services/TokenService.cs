using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectHK3.Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectHK3.Infrastructure.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        readonly IConfiguration _configuration = configuration;
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateAccessToken(int Id, string Email, string Role = "Employee", IEnumerable<Claim>? extraClaims = null)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, Email),
                new Claim(ClaimTypes.Role, Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (extraClaims != null)
                authClaims.AddRange(extraClaims);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT: Issuer"],
                audience: _configuration["JWT: Audience"],
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_configuration["JWT: AccessTokenExpirationMinutes"])
                ),
                claims: authClaims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
