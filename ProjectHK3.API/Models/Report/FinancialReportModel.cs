namespace ProjectHK3.Api.Models.Report
{
    public class FinancialReportModel
    {
        public object Id { get; set; } = default!;
        public object ApprovalId { get; set; } = default!;
        public object Amount { get; set; } = default!;
        public object Method { get; set; } = default!;
        public object Status { get; set; } = default!;
        public DateOnly TransactionDate { get; set; }
    }
}
