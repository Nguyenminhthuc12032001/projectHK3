using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Report;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Domain.Entities;
namespace ProjectHK3.Application.Implements.Services
{
    public class ReportService(
        IPolicyRequestDetailRepo policyRequestDetailRepo,
        IPolicyApprovalDetailRepo policyApprovalDetailRepo,
        ITransactionLedgerRepo transactionLedgerRepo,
        IPoliciesOnEmployeeRepo policiesOnEmployeeRepo,
        IPolicyRepo policyRepo,
        IEmpRegisterRepo empRegisterRepo,
        IAuditTrailRepo auditTrailRepo,
        IUnitOfWork unitOfWork
        ) : IReportService
    {
        readonly IPolicyRequestDetailRepo _policyRequestDetailRepo = policyRequestDetailRepo;
        readonly IPolicyApprovalDetailRepo _policyApprovalDetailRepo = policyApprovalDetailRepo;
        readonly ITransactionLedgerRepo _transactionLedgerRepo = transactionLedgerRepo;
        readonly IPoliciesOnEmployeeRepo _policiesOnEmployeeRepo = policiesOnEmployeeRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IEmpRegisterRepo _empRegisterRepo = empRegisterRepo;
        readonly IAuditTrailRepo _auditTrailRepo = auditTrailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(DateOnly from, DateOnly to)
        {
            var requests = await _policyRequestDetailRepo.GetAllAsync();

            var claims = requests.Where(r =>
                r.RequestType == RequestTypeOfPolicyRequestDetail.Claim &&
                r.RequestDate >= from &&
                r.RequestDate <= to &&
                !r.IsDeleted
            );

            return claims.Select(c => new ClaimReportDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                PolicyId = c.PolicyId,
                RequestDate = c.RequestDate,
                Status = c.Status.ToString(),
                Remarks = c.Remarks
            });
        }

        public async Task<IEnumerable<ClaimeRateReportDto>> GetClaimRatePolicyAsync()
        {
            var requests = await _policyRequestDetailRepo.GetAllAsync();
            var policies = await _policyRepo.GetAllAsync();

            var claimsByPolicy = requests
                .Where(r => r.RequestType == RequestTypeOfPolicyRequestDetail.Claim && !r.IsDeleted)
                .GroupBy(r => r.PolicyId);

            return claimsByPolicy.Select(g =>
            {
                var policy = policies.FirstOrDefault(p => p.Id == g.Key);
                var totalClaims = g.Count();
                var approvedClaims = g.Count(r => r.Status == StatusOfPolicyRequestDetail.Approved);

                return new ClaimeRateReportDto
                {
                    PolicyId = g.Key,
                    PolicyName = policy?.PolicyName ?? "Unknown",
                    TotalClaims = totalClaims,
                    ApprovedClaims = approvedClaims,
                    ClaimRate = (float)(totalClaims > 0 ? (decimal)approvedClaims / totalClaims * 100 : 0)
                };
            });
        }

        public async Task<IEnumerable<EmployeeCoverageReportDto>> GetCoverageByDepartmentAsync()
        {
            var employees = await _empRegisterRepo.GetAllAsync();
            var policiesOnEmployees = await _policiesOnEmployeeRepo.GetAllAsync();

            var departmentGroups = employees
                .Where(e => !e.IsDeleted)
                .GroupBy(e => e.Department ?? "Unknown");

            return departmentGroups.Select(g =>
            {
                var deptEmployees = g.ToList();
                var coveredEmployees = deptEmployees.Count(e =>
                    policiesOnEmployees.Any(poe =>
                        poe.EmployeeId == e.Id &&
                        poe.Status == StatusOfPoliciesOnEmployee.Active &&
                        !poe.IsDeleted
                    )
                );

                return new EmployeeCoverageReportDto
                {
                    Department = g.Key,
                    TotalEmployees = deptEmployees.Count,
                    CoveredEmployees = coveredEmployees,
                    CoverageRate = deptEmployees.Count > 0
                        ? (decimal)coveredEmployees / deptEmployees.Count * 100
                        : 0
                };
            });
        }

        public async Task<ReportSummaryDto> GetDashboardSummaryAsync()
        {
            var employees = await _empRegisterRepo.GetAllAsync();
            var policies = await _policyRepo.GetAllAsync();
            var requests = await _policyRequestDetailRepo.GetAllAsync();
            var transactions = await _transactionLedgerRepo.GetAllAsync();

            var activeEmployees = employees.Count(e =>
                e.Status == StatusOfEmpRegister.Active && !e.IsDeleted
            );

            var activePolicies = policies.Count(p =>
                p.Status == StatusOfPolicy.Active && !p.IsDeleted
            );

            var pendingClaims = requests.Count(r =>
                r.RequestType == RequestTypeOfPolicyRequestDetail.Claim &&
                r.Status == StatusOfPolicyRequestDetail.Pending &&
                !r.IsDeleted
            );

            var totalDisbursed = transactions
                .Where(t => t.Status == StatusOfTransactionLedger.Completed && !t.IsDeleted)
                .Sum(t => t.Amount);

            return new ReportSummaryDto
            {
                TotalEmployees = activeEmployees,
                TotalActivePolicies = activePolicies,
                PendingClaims = pendingClaims,
                TotalDisbursed = totalDisbursed
            };
        }

        public async Task<IEnumerable<FinancialReportDto>> GetFinancialReportAsync(DateOnly from, DateOnly to)
        {
            var transactions = await _transactionLedgerRepo.GetAllAsync();

            var periodTransactions = transactions.Where(t =>
                DateOnly.FromDateTime(t.CreatedAt.Date) >= from &&
                DateOnly.FromDateTime(t.CreatedAt.Date) <= to &&
                !t.IsDeleted
            );


            return periodTransactions.Select(t => new FinancialReportDto
            {
                Id = t.Id,
                ApprovalId = t.ApprovalId,
                Amount = t.Amount,
                Method = t.Method.ToString(),
                Status = t.Status.ToString(),
                TransactionDate = DateOnly.FromDateTime(t.CreatedAt.Date)
            });
        }

        public async Task<IEnumerable<PolicyReportDto>> GetPolicyReportAsync(DateOnly from, DateOnly to)
        {
            var requests = await _policyRequestDetailRepo.GetAllAsync();

            var enrollments = requests.Where(r =>
                r.RequestType == RequestTypeOfPolicyRequestDetail.Enrollment &&
                r.RequestDate >= from &&
                r.RequestDate <= to &&
                !r.IsDeleted
            );

            return enrollments.Select(e => new PolicyReportDto
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                PolicyId = e.PolicyId,
                RequestDate = e.RequestDate,
                Status = e.Status.ToString(),
                RequestType = e.RequestType.ToString()
            });
        }

        public async Task<IEnumerable<AuditReportDto>> GetSystemAuditReportAsync(DateOnly from, DateOnly to)
        {
            var audits = await _auditTrailRepo.GetAllAsync();

            var periodAudits = audits.Where(a =>
                DateOnly.FromDateTime(a.CreatedAt.Date) >= from &&
                DateOnly.FromDateTime(a.CreatedAt.Date) <= to &&
                !a.IsDeleted
            );

            return periodAudits.Select(a => new AuditReportDto
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                AdminId = a.AdminId,
                Action = a.Action,
                TableName = a.TableName,
                RecordId = a.RecordId,
                Details = a.Details,
                CreatedAt = a.CreatedAt
            });
        }

    }
}
