namespace ProjectHK3.Api.Models.PolicyRequest
{
    public class UpdatePolicyRequestModel
    {
        public string? Status { get; set; }  // Pending | Approved | Rejected
        public string? Remarks { get; set; }
    }
}
