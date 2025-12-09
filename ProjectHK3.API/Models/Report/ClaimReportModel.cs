namespace ProjectHK3.Api.Models.Report
{
    public class ClaimReportModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public int PolicyId { get; set; }
    }
}
