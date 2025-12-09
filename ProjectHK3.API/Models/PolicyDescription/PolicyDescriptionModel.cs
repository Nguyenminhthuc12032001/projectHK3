namespace ProjectHK3.Api.Models.PolicyDescription
{
    public class PolicyDescriptionModel
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public string? PolicySummary { get; set; }
        public string? Benefits { get; set; }
        public string? Exclusions { get; set; }
        public string? TermsConditions { get; set; }
    }
}
