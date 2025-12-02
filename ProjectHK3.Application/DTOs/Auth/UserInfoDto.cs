using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.DTOs.Auth
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "Employee";
        
    }
}
