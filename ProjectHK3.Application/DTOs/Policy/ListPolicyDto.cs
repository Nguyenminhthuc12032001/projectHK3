namespace ProjectHK3.Application.DTOs.Policy
{
    public class ListPolicyDto
    {
        public int Id { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
    }
}
