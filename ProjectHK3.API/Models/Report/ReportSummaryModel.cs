namespace ProjectHK3.Api.Models.Report
{
    public class ReportSummaryModel
    {
        public int TotalEmployees { get; set; }
        public int TotalActivePolicies { get; set; }
        public int PendingClaims { get; set; }
        public decimal TotalDisbursed { get; set; }
    }
}
