namespace ProjectHK3.Application.DTOs.Policy
{
    public class CreatePolicyDto
    {
        public int CompanyId { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateOnly EffectiveFrom { get; set; }
        public DateOnly EffectiveTo { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
