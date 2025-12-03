namespace ProjectHK3.Api.Models.Auth
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
