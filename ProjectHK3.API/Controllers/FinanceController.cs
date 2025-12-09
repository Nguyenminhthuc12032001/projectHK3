using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Finance;
using ProjectHK3.Application.Abstractions.IServices;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceService _financeService;
        private readonly IMapper _mapper;
        private readonly ILogger<FinanceController> _logger;

        public FinanceController(
            IFinanceService financeService,
            IMapper mapper,
            ILogger<FinanceController> logger)
        {
            _financeService = financeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("create/{claimId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDisbursement(int claimId)
        {
            var result = await _financeService.CreateDisbursementRequestAsync(claimId);

            if (!result)
                return BadRequest("Unable to create disbursement request. Maybe already exists or claim not approved.");

            return Ok("Disbursement request created successfully.");
        }

        [HttpPut("{transactionId:int}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveDisbursement(int transactionId)
        {
            var adminId = 1; 
            var result = await _financeService.ApproveDisbursementAsync(transactionId, adminId);

            if (!result)
                return BadRequest("Approval failed.");

            return Ok("Approved successfully, funds reserved.");
        }

        [HttpPut("{transactionId:int}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectDisbursement(int transactionId, [FromQuery] string? remarks)
        {
            var adminId = 1;
            var result = await _financeService.RejectDisbursementAsync(transactionId, adminId, remarks);

            if (!result)
                return BadRequest("Rejection failed.");

            return Ok("Rejection processed.");
        }

        [HttpPut("{transactionId:int}/execute")]
        [Authorize(Roles = "Admin,Finance")]
        public async Task<IActionResult> ExecutePayment(int transactionId)
        {
            var result = await _financeService.ExecutePaymentAsync(transactionId);

            if (!result)
                return BadRequest("Payment execution failed.");

            return Ok("Payment completed successfully.");
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin,Finance")]
        public async Task<IActionResult> GetPending()
        {
            var list = await _financeService.GetPendingDisbursementsAsync();
            return Ok(_mapper.Map<IEnumerable<DisbursementListModel>>(list));
        }

        [HttpGet("history/{claimId:int}")]
        public async Task<IActionResult> GetHistory(int claimId)
        {
            var history = await _financeService.GetTransactionHistoryAsync(claimId);
            return Ok(_mapper.Map<IEnumerable<TransactionHistoryModel>>(history));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _financeService.GetDisbursementByIdAsync(id);
            if (detail == null) return NotFound("Transaction not found.");

            return Ok(_mapper.Map<DisbursementDetailModel>(detail));
        }

        [HttpGet("calculate/{claimId:int}")]
        [Authorize(Roles = "Admin,Finance")]
        public async Task<IActionResult> CalculateFinal(int claimId)
        {
            var result = await _financeService.CalculateFinalAmountAsync(claimId);
            return Ok(result);
        }
    }
}
