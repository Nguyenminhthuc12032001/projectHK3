namespace ProjectHK3.Api.Models.PolicyRequest
{
    public class CreatePolicyRequestModel
    {
        public int EmployeeId { get; set; }
        public int PolicyId { get; set; }
        public string RequestType { get; set; } = string.Empty; // Enrollment | Claim | Cancel
        public string? Remarks { get; set; }
    }
}
