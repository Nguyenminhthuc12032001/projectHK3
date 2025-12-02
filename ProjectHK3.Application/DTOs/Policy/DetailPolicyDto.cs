namespace ProjectHK3.Application.DTOs.Policy
{
    public class DetailPolicyDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;

        public string PolicyName { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateOnly EffectiveFrom { get; set; }
        public DateOnly EffectiveTo { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
