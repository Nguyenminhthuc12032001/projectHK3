namespace ProjectHK3.Application.DTOs.Auth
{
    public class AuthValidateResponse
    {
        public int EmpId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
