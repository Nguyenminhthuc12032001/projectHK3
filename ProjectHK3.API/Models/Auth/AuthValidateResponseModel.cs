namespace ProjectHK3.Api.Models.Auth
{
    public class AuthValidateResponseModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
