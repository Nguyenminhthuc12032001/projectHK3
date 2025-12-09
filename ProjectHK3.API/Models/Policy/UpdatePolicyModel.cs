namespace ProjectHK3.Api.Models.Policy
{
    public class UpdatePolicyModel
    {
        public string? PolicyName { get; set; }
        public decimal? CoverageAmount { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public string? Status { get; set; }
    }
}
