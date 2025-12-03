namespace ProjectHK3.Application.DTOs.Report
{
    public class EmployeeCoverageReportDto
    {
        public string Department { get; internal set; }
        public int TotalEmployees { get; internal set; }
        public decimal CoverageRate { get; internal set; }
        public int CoveredEmployees { get; internal set; }
    }
}
