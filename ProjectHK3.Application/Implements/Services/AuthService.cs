using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Auth;

namespace ProjectHK3.Application.Implements.Services
{
    public class AuthService : IAuthService
    {
        public Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            throw new NotImplementedException();
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
