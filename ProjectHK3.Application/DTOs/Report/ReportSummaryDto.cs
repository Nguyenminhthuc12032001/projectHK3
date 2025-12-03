namespace ProjectHK3.Application.DTOs.Report
{
    public class ReportSummaryDto
    {
        public int TotalEmployees { get; internal set; }
        public int TotalActivePolicies { get; internal set; }
        public int PendingClaims { get; internal set; }
        public decimal TotalDisbursed { get; internal set; }
    }
}
