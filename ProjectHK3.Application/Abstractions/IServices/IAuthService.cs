using ProjectHK3.Application.DTOs.Auth;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<bool> LogOutAsync(string refreshToken);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<bool> AddNewAdminAsync(NewAdminRequest request);
        Task<bool> ChangePasswordAdminAsync(ChangePasswordRequest.Admin request);
        Task<bool> ChangePasswordEmployeeAsync(ChangePasswordRequest.Employee request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<AuthValidateResponse?> ValidateTokenAsync(string token);
        Task<UserInfoDto?> GetCurrentUserAsync();
    }
}
