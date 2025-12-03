
namespace ProjectHK3.Application.DTOs.PolicyApproval
{
    public class PolicyApprovalDetailDto
    {
        public int Id { get; internal set; }
        public int RequestId { get; internal set; }
        public int AdminId { get; internal set; }
        public DateOnly ApprovalDate { get; internal set; }
        public string? Remarks { get; internal set; }
        public string Status { get; internal set; }
        public string PaymentStatus { get; internal set; }
        public DateTimeOffset CreatedAt { get; internal set; }
    }
}
