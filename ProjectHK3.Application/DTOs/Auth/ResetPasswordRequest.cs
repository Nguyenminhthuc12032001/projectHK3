namespace ProjectHK3.Application.DTOs.Auth
{
    public class ResetPasswordRequest
    {
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
