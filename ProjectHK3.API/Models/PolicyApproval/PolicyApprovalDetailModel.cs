namespace ProjectHK3.Api.Models.PolicyApproval
{
    public class PolicyApprovalDetailModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AdminId { get; set; }
        public DateOnly ApprovalDate { get; set; }
        public string? Remarks { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
