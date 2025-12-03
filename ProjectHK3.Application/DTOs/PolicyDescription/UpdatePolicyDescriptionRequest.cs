namespace ProjectHK3.Application.DTOs.PolicyDescription
{
    public class UpdatePolicyDescriptionRequest
    {
        public int Id { get; set; }

        public string? PolicySummary { get; set; }
        public string? Benefits { get; set; }
        public string? Exclusions { get; set; }
        public string? TermsConditions { get; set; }
    }
}
