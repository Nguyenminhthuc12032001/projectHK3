namespace ProjectHK3.Api.Models.Report
{
    public class EmployeeCoverageReportModel
    {
        public string Department { get; set; } = string.Empty;
        public int TotalEmployees { get; set; }
        public decimal CoverageRate { get; set; }
        public int CoveredEmployees { get; set; }
    }
}
