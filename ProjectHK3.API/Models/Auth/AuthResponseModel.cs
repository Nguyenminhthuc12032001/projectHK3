namespace ProjectHK3.Api.Models.Auth
{
    public class AuthResponseModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
