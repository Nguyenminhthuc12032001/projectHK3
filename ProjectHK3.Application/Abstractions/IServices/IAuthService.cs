using ProjectHK3.Application.DTOs.Auth;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<bool> LogOutAsync(string refreshToken);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<AuthValidateResponse?> ValidateTokenAsync(string token);
        Task<UserInfoDto?> GetCurrentUserAsync();
    }
}
