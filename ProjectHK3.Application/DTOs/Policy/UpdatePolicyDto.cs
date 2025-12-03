namespace ProjectHK3.Application.DTOs.Policy
{
    public class UpdatePolicyDto
    {
        public int Id { get; set; }
        public string? PolicyName { get; set; }
        public decimal? CoverageAmount { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public string? Status { get; set; }
    }
}
