namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IReportService
    {
        Task<ReportSummaryDto> GetDashboardSummaryAsync();
        Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(DateOnly from, DateOnly to);
        Task<IEnumerable<PolicyReportDto>> GetPolicyReportAsync(DateOnly from, DateOnly to);
        Task<IEnumerable<FinancialReportDto>> GetFinancialReportAsync(DateOnly from, DateOnly to);
        Task<IEnumerable<EmployeeCoverageReportDto>> GetCoverageByDepartmentAsync();
        Task<IEnumerable<ClaimeRateReportDto>> GetClaimRatePolicyAsync();
        Task<IEnumerable<AuditReportDto>> GetSystemAuditReportAsync(DateOnly from, DateOnly to);
    }
}
