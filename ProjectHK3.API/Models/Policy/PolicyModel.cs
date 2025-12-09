namespace ProjectHK3.Api.Models.Policy
{
    public class PolicyModel
    {
        public int Id { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
    }
}
