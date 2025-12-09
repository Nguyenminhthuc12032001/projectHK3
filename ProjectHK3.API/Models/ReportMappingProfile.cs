using AutoMapper;
using ProjectHK3.Api.Models.Report;
using ProjectHK3.Application.DTOs.Report;
using ProjectHK3.Domain.Entities;

public class ReportMappingProfile : Profile
{
    public ReportMappingProfile()
    {
        // ReportLog - Create
        CreateMap<CreateReportLogModel, ReportLog>();

        CreateMap<ReportLog, ReportLogModel>();

        // Analytics report mapping
        CreateMap<AuditReportDto, AuditReportModel>();
        CreateMap<ClaimeRateReportDto, ClaimeRateReportModel>();
        CreateMap<ClaimReportDto, ClaimReportModel>();
        CreateMap<EmployeeCoverageReportDto, EmployeeCoverageReportModel>();
        CreateMap<FinancialReportDto, FinancialReportModel>();
        CreateMap<PolicyReportDto, PolicyReportModel>();
        CreateMap<ReportSummaryDto, ReportSummaryModel>();
    }
}
