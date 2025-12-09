namespace ProjectHK3.Api.Models.Report
{
    public class CreateReportLogModel
    {
        public string ReportType { get; set; } = string.Empty;
        public int GeneratedBy { get; set; } // AdminId
    }
}
