namespace ProjectHK3.Api.Models.PolicyApproval
{
    public class PolicyRequestListModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PolicyId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public DateOnly RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
    }
}
