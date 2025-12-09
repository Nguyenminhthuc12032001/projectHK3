using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Auth;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Auth;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var request = _mapper.Map<LoginRequest>(model);
            var result = await _authService.LoginAsync(request)
                ?? throw new BusinessException("Invalid email or password", 401);

            return Ok(_mapper.Map<AuthResponseModel>(result));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var request = _mapper.Map<RegisterRequest>(model);
            var success = await _authService.RegisterAsync(request);

            if (!success)
                throw new BusinessException("Email or phone already exists", 409);

            return Ok(new { message = "Registration successful, please verify email." });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("admin/add")]
        public async Task<IActionResult> AddNewAdmin(NewAdminModel model)
        {
            var request = _mapper.Map<NewAdminRequest>(model);
            var success = await _authService.AddNewAdminAsync(request);

            if (!success)
                throw new BusinessException("Admin email already exists", 409);

            return Ok(new { message = "New admin created successfully" });
        }

        [Authorize(Roles = "Admin,Manager,Finance")]
        [HttpPost("change-password/admin")]
        public async Task<IActionResult> ChangePasswordAdmin(ChangePasswordModel.Admin model)
        {
            var request = _mapper.Map<ChangePasswordRequest.Admin>(model);
            var success = await _authService.ChangePasswordAdminAsync(request);

            if (!success)
                throw new BusinessException("Password change failed", 400);

            return Ok(new { message = "Password changed successfully" });
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("change-password/employee")]
        public async Task<IActionResult> ChangePasswordEmployee(ChangePasswordModel.Employee model)
        {
            var request = _mapper.Map<ChangePasswordRequest.Employee>(model);
            var success = await _authService.ChangePasswordEmployeeAsync(request);

            if (!success)
                throw new BusinessException("Password change failed", 400);

            return Ok(new { message = "Password changed successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            var success = await _authService.ForgotPasswordAsync(email);

            if (!success)
                throw new BusinessException("Email not found", 404);

            return Ok(new { message = "Reset password link sent to email" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var request = _mapper.Map<ResetPasswordRequest>(model);
            var success = await _authService.ResetPasswordAsync(request);

            if (!success)
                throw new BusinessException("Invalid or expired token", 400);

            return Ok(new { message = "Password reset successfully" });
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _authService.GetCurrentUserAsync()
                ?? throw new BusinessException("User not found", 404);

            return Ok(_mapper.Map<UserInfoModel>(user));
        }

        [HttpGet("validate")]
        public async Task<IActionResult> ValidateToken([FromQuery] string token)
        {
            var result = await _authService.ValidateTokenAsync(token)
                ?? throw new BusinessException("Invalid or expired verification token", 400);

            return Ok(_mapper.Map<AuthValidateResponseModel>(result));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string refreshToken)
        {
            var success = await _authService.LogOutAsync(refreshToken);

            if (!success)
                throw new BusinessException("Logout failed", 400);

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken.RefreshToken);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            return Ok(result);
        }
    }
}
