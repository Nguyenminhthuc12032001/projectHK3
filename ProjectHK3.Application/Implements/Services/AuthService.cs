using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Auth;
using ProjectHK3.Domain.Abstractions;

namespace ProjectHK3.Application.Implements.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IAdminLoginRepo adminLoginRepo, IAuthSessionRepo authSessionRepo) : IAuthService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IAdminLoginRepo _adminLoginRepo = adminLoginRepo;
        readonly IAuthSessionRepo _authSessionRepo = authSessionRepo;
        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var match = await _
        }

        public Task<bool> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto?> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOutAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthValidateResponse?> ValidateTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
