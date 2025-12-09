namespace ProjectHK3.Api.Models.PolicyApproval
{
    public class PolicyApprovalRequestModel
    {
        public int PolicyRequestId { get; set; }
        public int AdminId { get; set; }
        public string? Remarks { get; set; }
    }
}
