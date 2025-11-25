using Microsoft.AspNetCore.Http;
using ProjectHK3.Application.Abstractions;
using System.Security.Claims;

namespace ProjectHK3.Infrastructure.Services
{
    public class CurrentUserService(HttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? "System";
    }
}
