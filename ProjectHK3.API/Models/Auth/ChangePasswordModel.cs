namespace ProjectHK3.Api.Models.Auth
{
    public class ChangePasswordModel
    {
        public class Admin
        {
            public int AdminId { get; set; }
            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }

        public class Employee
        {
            public int EmployeeId { get; set; }
            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }
    }
}
