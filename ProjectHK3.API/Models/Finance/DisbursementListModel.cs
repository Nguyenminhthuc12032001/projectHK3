namespace ProjectHK3.Api.Models.Finance
{
    public class DisbursementListModel
    {
        public int Id { get; set; }
        public int ApprovalId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
