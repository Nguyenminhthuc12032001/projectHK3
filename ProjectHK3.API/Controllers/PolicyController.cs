using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Policy;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Policy;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController(IPolicyService policyService, IMapper mapper) : ControllerBase
    {
        private readonly IPolicyService _policyService = policyService;
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllPolicies([FromQuery] bool includeDeleted = false)
        {
            var list = await _policyService.GetAllPoliciesAsync(includeDeleted);
            return Ok(_mapper.Map<IEnumerable<PolicyModel>>(list));
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet("{policyId:int}")]
        public async Task<IActionResult> GetPolicyById(int policyId)
        {
            var detail = await _policyService.GetPolicyByIdAsync(policyId)
                ?? throw new BusinessException("Policy not found", 404);

            return Ok(_mapper.Map<PolicyDetailModel>(detail));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyModel model)
        {
            var request = _mapper.Map<CreatePolicyDto>(model);
            var id = await _policyService.CreatePolicyAsync(request);

            return Ok(new { message = "Policy created successfully", policyId = id });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{policyId:int}")]
        public async Task<IActionResult> UpdatePolicy(int policyId, [FromBody] UpdatePolicyModel model)
        {
            var request = _mapper.Map<UpdatePolicyDto>(model);
            var success = await _policyService.UpdatePolicyAsync(policyId, request);

            if (!success)
                throw new BusinessException("Policy update failed", 400);

            return Ok(new { message = "Policy updated successfully" });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{policyId:int}")]
        public async Task<IActionResult> SoftDeletePolicy(int policyId)
        {
            var success = await _policyService.SoftDeletePolicyAsync(policyId);

            if (!success)
                throw new BusinessException("Policy delete failed", 400);

            return Ok(new { message = "Policy deleted successfully" });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{policyId:int}/restore")]
        public async Task<IActionResult> RestorePolicy(int policyId)
        {
            var success = await _policyService.RestorePolicyAsync(policyId);

            if (!success)
                throw new BusinessException("Restore policy failed", 400);

            return Ok(new { message = "Policy restored successfully" });
        }
    }
}
