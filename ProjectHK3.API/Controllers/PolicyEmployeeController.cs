using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.PolicyEmployee;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyEmployee;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyEmployeeController : ControllerBase
    {
        private readonly IPolicyEmployeeService _service;
        private readonly IMapper _mapper;

        public PolicyEmployeeController(IPolicyEmployeeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetPoliciesByEmployee(int employeeId)
        {
            var result = await _service.GetPoliciesByEmployeeAsync(employeeId);
            return Ok(_mapper.Map<IEnumerable<PolicyEmployeeModel>>(result));
        }

        [HttpGet("active/{employeeId}")]
        public async Task<IActionResult> GetActivePolicies(int employeeId)
        {
            var result = await _service.GetActivePoliciesByEmployeeAsync(employeeId);
            return Ok(_mapper.Map<IEnumerable<PolicyEmployeeModel>>(result));
        }

        [HttpGet("check-active")]
        public async Task<IActionResult> CheckPolicyActive(int employeeId, int policyId)
        {
            var isActive = await _service.IsPolicyActiveForEmployeeAsync(employeeId, policyId);
            return Ok(new { IsActive = isActive });
        }

        // 🔹 POST: api/PolicyEmployee
        [HttpPost]
        public async Task<IActionResult> AssignPolicy([FromBody] CreatePolicyEmployeeModel model)
        {
            var dto = _mapper.Map<PolicyEmployeeDto>(model);

            var id = await _service.AssignPolicyToEmployeeAsync(
                dto.EmployeeId,
                dto.PolicyId,
                dto.StartDate,
                dto.EndDate
            );

            if (id == null)
                return Conflict("Employee already has this policy in active duration.");

            return CreatedAtAction(nameof(GetPoliciesByEmployee),
                new { employeeId = dto.EmployeeId },
                new { Id = id }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicyDuration(
            int id,
            [FromBody] UpdatePolicyEmployeeModel model)
        {
            if (model.EndDate == null)
                return BadRequest("EndDate is required.");

            var success = await _service.UpdatePolicyDurationAsync(
                id,
                DateOnly.FromDateTime(DateTime.Now),
                model.EndDate.Value
            );

            if (!success)
                return NotFound("Policy employee record not found.");

            return Ok("Updated successfully.");
        }

        [HttpDelete("{employeeId}/{policyId}")]
        public async Task<IActionResult> RemovePolicy(int employeeId, int policyId)
        {
            var success = await _service.RemovePolicyFromEmployeeAsync(employeeId, policyId);

            if (!success)
                return NotFound("No matching policy found.");

            return Ok("Policy removed successfully.");
        }
    }
}
