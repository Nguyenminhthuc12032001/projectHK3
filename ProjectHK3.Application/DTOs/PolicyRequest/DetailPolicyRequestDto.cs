namespace ProjectHK3.Application.DTOs.PolicyRequest
{
    public class DetailPolicyRequestDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public string PolicyName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly RequestDate { get; set; }
        public string? Remarks { get; set; }
        public IEnumerable<PolicyRequestDocumentDto>? Documents { get; set; }

    }
}
