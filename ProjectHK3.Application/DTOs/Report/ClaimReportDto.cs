
namespace ProjectHK3.Application.DTOs.Report
{
    public class ClaimReportDto
    {
        public int Id { get; internal set; }
        public int EmployeeId { get; internal set; }
        public DateOnly RequestDate { get; internal set; }
        public string Status { get; internal set; }
        public string? Remarks { get; internal set; }
        public int PolicyId { get; internal set; }
    }
}
