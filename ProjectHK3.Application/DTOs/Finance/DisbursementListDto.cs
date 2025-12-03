
namespace ProjectHK3.Application.DTOs.Finance
{
    public class DisbursementListDto
    {
        public int Id { get; internal set; }
        public int ApprovalId { get; internal set; }
        public decimal Amount { get; internal set; }
        public string Method { get; internal set; }
        public string Status { get; internal set; }
        public DateTimeOffset CreatedAt { get; internal set; }
    }
}
