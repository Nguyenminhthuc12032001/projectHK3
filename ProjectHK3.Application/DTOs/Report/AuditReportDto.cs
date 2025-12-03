namespace ProjectHK3.Application.DTOs.Report
{
    public class AuditReportDto
    {
        public object Id { get; internal set; }
        public object EmployeeId { get; internal set; }
        public object CreatedAt { get; internal set; }
        public int AdminId { get; internal set; }
        public string? Action { get; internal set; }
        public string? TableName { get; internal set; }
        public int RecordId { get; internal set; }
        public string? Details { get; internal set; }
    }
}
