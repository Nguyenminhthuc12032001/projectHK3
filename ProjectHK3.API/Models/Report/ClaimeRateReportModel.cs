namespace ProjectHK3.Api.Models.Report
{
    public class ClaimeRateReportModel
    {
        public int TotalRecords { get; set; }
        public int ApprovedRecords { get; set; }
        public float ClaimRate { get; set; }
        public int ApprovedClaims { get; set; }
        public int TotalClaims { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public int PolicyId { get; set; }
    }
}
