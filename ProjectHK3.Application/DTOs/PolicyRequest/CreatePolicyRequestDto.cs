namespace ProjectHK3.Application.DTOs.PolicyRequest
{
    public class CreatePolicyRequestDto
    {
        public int PolicyId { get; set; }
        public int EmployeeId { get; set; }
        public string RequestType { get; set; } = string.Empty;// "Enrollment" | "Claim" | "Cancel"
        public string? Remarks { get; set; }
    }
}
