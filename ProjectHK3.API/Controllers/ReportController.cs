using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Report;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(
            IReportService reportService,
            IMapper mapper,
            ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                var data = await _reportService.GetDashboardSummaryAsync();
                var result = _mapper.Map<ReportSummaryModel>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaimReport([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            try
            {
                if (from > to)
                    return BadRequest("Invalid date range.");

                var data = await _reportService.GetClaimReportAsync(from, to);
                var result = data.Select(_mapper.Map<ClaimReportModel>);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("claims/rate")]
        public async Task<IActionResult> GetClaimRatePolicyReport()
        {
            try
            {
                var data = await _reportService.GetClaimRatePolicyAsync();
                var result = data.Select(_mapper.Map<ClaimeRateReportModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("coverage/department")]
        public async Task<IActionResult> GetCoverageByDepartmentReport()
        {
            try
            {
                var data = await _reportService.GetCoverageByDepartmentAsync();
                var result = data.Select(_mapper.Map<EmployeeCoverageReportModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("financial")]
        public async Task<IActionResult> GetFinancialReport([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            try
            {
                if (from > to)
                    return BadRequest("Invalid date range.");

                var data = await _reportService.GetFinancialReportAsync(from, to);
                var result = data.Select(_mapper.Map<FinancialReportModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("policy")]
        public async Task<IActionResult> GetPolicyReport([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            try
            {
                if (from > to)
                    return BadRequest("Invalid date range.");

                var data = await _reportService.GetPolicyReportAsync(from, to);
                var result = data.Select(_mapper.Map<PolicyReportModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("audit")]
        public async Task<IActionResult> GetSystemAuditReport([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            try
            {
                if (from > to)
                    return BadRequest("Invalid date range.");

                var data = await _reportService.GetSystemAuditReportAsync(from, to);
                var result = data.Select(_mapper.Map<AuditReportModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
