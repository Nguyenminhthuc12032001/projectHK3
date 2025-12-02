namespace ProjectHK3.Application.DTOs.PolicyDescription
{
    public class PolicyDescriptionDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public int PolicyId { get; set; }
        public string? PolicySummary { get; set; }
        public string? Benefits { get; set; }
        public string? Exclusions { get; set; }
        public string? TermsConditions { get; set; }
    }
}
