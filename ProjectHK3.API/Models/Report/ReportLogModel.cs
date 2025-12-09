namespace ProjectHK3.Api.Models.Report
{
    public class ReportLogModel
    {
        public int Id { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public int GeneratedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
