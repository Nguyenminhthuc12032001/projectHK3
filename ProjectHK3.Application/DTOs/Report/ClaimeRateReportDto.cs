namespace ProjectHK3.Application.DTOs.Report
{
    public class ClaimeRateReportDto
    {
        public int TotalRecords { get; internal set; }
        public int ApprovedRecords { get; internal set; }
        public float ClaimRate { get; internal set; }
    }
}
