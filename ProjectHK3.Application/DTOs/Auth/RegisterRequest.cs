namespace ProjectHK3.Application.DTOs.Auth
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string HireDate { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
