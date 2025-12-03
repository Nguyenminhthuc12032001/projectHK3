
namespace ProjectHK3.Application.DTOs.Report
{
    public class FinancialReportDto
    {
        public object Id { get; internal set; }
        public object ApprovalId { get; internal set; }
        public object Amount { get; internal set; }
        public object Method { get; internal set; }
        public object Status { get; internal set; }
        public DateOnly TransactionDate { get; internal set; }
    }
}
