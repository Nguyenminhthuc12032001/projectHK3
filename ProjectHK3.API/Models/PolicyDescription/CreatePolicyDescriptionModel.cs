namespace ProjectHK3.Api.Models.PolicyDescription
{
    public class CreatePolicyDescriptionModel
    {
        public string PolicySummary { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public string Exclusions { get; set; } = string.Empty;
        public string TermsConditions { get; set; } = string.Empty;
    }
}
