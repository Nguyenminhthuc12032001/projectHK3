namespace ProjectHK3.Api.Models.Auth
{
    public class ResetPasswordModel
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
