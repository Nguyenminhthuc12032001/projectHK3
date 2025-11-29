namespace ProjectHK3.Application.DTOs.Auth
{
    public class ChangePasswordRequest
    {
        public int EmployeeId { get; set; }
        public int AdminId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
