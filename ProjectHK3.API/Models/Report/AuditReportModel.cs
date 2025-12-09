namespace ProjectHK3.Api.Models.Report
{
    public class AuditReportModel
    {
        public object Id { get; set; }
        public object EmployeeId { get; set; }
        public object CreatedAt { get; set; }
        public int AdminId { get; set; }
        public string? Action { get; set; }
        public string? TableName { get; set; }
        public int RecordId { get; set; }
        public string? Details { get; set; }
    }
}
