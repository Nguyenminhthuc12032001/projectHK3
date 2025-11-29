using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Report;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Application.Implements.Services
{
    public class ReportService(IUnitOfWork unitOfWork, IReportLogRepo reportlog, IPolicyApprovalDetailRepo PADR) : IReportService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IReportLogRepo _IReportLogRepo = reportlog;
        readonly IPolicyApprovalDetailRepo _IPolicyApprovalDetailRepo = PADR;
        public async Task<IEnumerable<ClaimeRateReportDto>> GetClaimRatePolicyAsync()
        {
            var content = await _IPolicyApprovalDetailRepo.GetAllAsync();

            int  totalrecords = content.Count();
            if (totalrecords == 0)
            {
                return Enumerable.Empty<ClaimeRateReportDto>();
            }

            int approvedCount = content.Count(x => x.Status == StatusOfPolicyApprovalDetail.Approved);
            if (approvedCount == 0)
            {
                return Enumerable.Empty<ClaimeRateReportDto>();
            }
            float claimRate = (float)approvedCount / totalrecords * 100f;


            return new List<ClaimeRateReportDto>
            {
                new ClaimeRateReportDto
                {
                    TotalRecords = totalrecords,
                    ApprovedRecords = approvedCount,
                    ClaimRate = claimRate
                }
            };
        }

        public Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(DateOnly from, DateOnly to)
        {
            
        }

        public Task<IEnumerable<EmployeeCoverageReportDto>> GetCoverageByDepartmentAsync(string? Department)
        {
            
        }

        public Task<ReportSummaryDto> GetDashboardSummaryAsync()
        {
            
        }

        public Task<IEnumerable<FinancialReportDto>> GetFinancialReportAsync(DateOnly from, DateOnly to)
        {
            
        }

        public Task<IEnumerable<PolicyReportDto>> GetPolicyReportAsync(DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditReportDto>> GetSystemAuditReportAsync(DateOnly from, DateOnly to)
        {
            throw new NotImplementedException();
        }
    }
}
