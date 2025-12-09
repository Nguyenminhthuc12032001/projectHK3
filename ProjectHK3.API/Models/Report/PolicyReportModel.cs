namespace ProjectHK3.Api.Models.Report
{
    public class PolicyReportModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PolicyId { get; set; }
        public DateOnly RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
    }
}
