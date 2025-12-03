
namespace ProjectHK3.Application.DTOs.PolicyApproval
{
    public class PolicyRequestListDto
    {
        public int Id { get; internal set; }
        public int EmployeeId { get; internal set; }
        public int PolicyId { get; internal set; }
        public string RequestType { get; internal set; }
        public DateOnly RequestDate { get; internal set; }
        public string Status { get; internal set; }
        public string? Remarks { get; internal set; }
    }
}
