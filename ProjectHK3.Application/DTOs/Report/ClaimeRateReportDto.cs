namespace ProjectHK3.Application.DTOs.Report
{
    public class ClaimeRateReportDto
    {
        public int TotalRecords { get; internal set; }
        public int ApprovedRecords { get; internal set; }
        public float ClaimRate { get; internal set; }
        public int ApprovedClaims { get; internal set; }
        public int TotalClaims { get; internal set; }
        public string PolicyName { get; internal set; }
        public int PolicyId { get; internal set; }
    }
}
