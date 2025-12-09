using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.PolicyApproval;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyApprovalController(IPolicyApprovalService policyApprovalService) : ControllerBase
    {
        private readonly IPolicyApprovalService _policyApprovalService = policyApprovalService;

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{policyRequestId:int}/approve")]
        public async Task<IActionResult> Approve(int policyRequestId, [FromBody] PolicyApprovalRequestModel model)
        {
            if (policyRequestId != model.PolicyRequestId)
                throw new BusinessException("Policy request mismatch.", 400);

            var success = await _policyApprovalService
                .ApprovePolicyAsync(model.PolicyRequestId, model.AdminId, model.Remarks ?? "");

            if (!success)
                throw new BusinessException("Approval failed.", 400);

            return Ok(new { message = "Policy approved successfully" });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{policyRequestId:int}/reject")]
        public async Task<IActionResult> Reject(int policyRequestId, [FromBody] PolicyApprovalRequestModel model)
        {
            if (policyRequestId != model.PolicyRequestId)
                throw new BusinessException("Policy request mismatch.", 400);

            var success = await _policyApprovalService
                .RejectPolicyAsync(model.PolicyRequestId, model.AdminId, model.Remarks ?? "");

            if (!success)
                throw new BusinessException("Rejection failed.", 400);

            return Ok(new { message = "Policy rejected successfully" });
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet("{policyRequestId:int}/history")]
        public async Task<IActionResult> GetHistory(int policyRequestId)
        {
            var list = await _policyApprovalService.GetApprovalHistoryAsync(policyRequestId);

            if (!list.Any())
                throw new BusinessException("No approval history found for this request.", 404);

            return Ok(list);
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet("{policyRequestId:int}/latest")]
        public async Task<IActionResult> GetLatest(int policyRequestId)
        {
            var latest = await _policyApprovalService.GetLastestApprovalAsync(policyRequestId)
                ?? throw new BusinessException("No approval record found.", 404);

            return Ok(latest);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var pending = await _policyApprovalService.GetPendingApprovalAsync();

            if (!pending.Any())
                return Ok(new { message = "No pending requests." });

            return Ok(pending);
        } 
    }
}
