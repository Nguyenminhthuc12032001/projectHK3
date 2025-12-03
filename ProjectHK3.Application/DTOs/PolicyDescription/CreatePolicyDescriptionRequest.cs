namespace ProjectHK3.Application.DTOs.PolicyDescription
{
    public class CreatePolicyDescriptionRequest
    {
        public int PolicyId { get; set; }
        public string PolicySummary { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public string Exclusions { get; set; } = string.Empty;
        public string TermsConditions { get; set; } = string.Empty;
    }
}
