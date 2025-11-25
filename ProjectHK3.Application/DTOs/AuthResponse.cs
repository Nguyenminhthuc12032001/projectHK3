namespace ProjectHK3.Application.DTOs
{
    internal class AuthResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireAt { get; set; }
        public string? TokenType { get; set; } = "Bearer";
    }
}
