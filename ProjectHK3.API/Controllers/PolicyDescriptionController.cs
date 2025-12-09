using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.PolicyDescription;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyDescription;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/policies/{policyId:int}/descriptions")]
    [ApiController]
    public class PolicyDescriptionController : ControllerBase
    {
        private readonly IPolicyDescriptionService _service;
        private readonly IMapper _mapper;

        public PolicyDescriptionController(IPolicyDescriptionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int policyId, [FromBody] CreatePolicyDescriptionModel model)
        {
            var request = _mapper.Map<CreatePolicyDescriptionRequest>(model);
            var id = await _service.AddDescriptionAsync(policyId, request);
            return CreatedAtAction(nameof(GetById), new { policyId, descriptionId = id }, id);
        }

        [HttpGet("{descriptionId:int}")]
        public async Task<IActionResult> GetById(int policyId, int descriptionId)
        {
            var result = await _service.GetByIdAsync(descriptionId);
            return Ok(_mapper.Map<PolicyDescriptionModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetByPolicy(int policyId)
        {
            var results = await _service.GetByPolicyAsync(policyId);
            return Ok(_mapper.Map<IEnumerable<PolicyDescriptionModel>>(results));
        }

        [HttpPut("{descriptionId:int}")]
        public async Task<IActionResult> Update(int policyId, int descriptionId, UpdatePolicyDescriptionModel model)
        {
            var request = _mapper.Map<UpdatePolicyDescriptionRequest>(model);
            await _service.UpdateDescriptionAsync(descriptionId, request);
            return NoContent();
        }

        [HttpDelete("{descriptionId:int}")]
        public async Task<IActionResult> Delete(int policyId, int descriptionId)
        {
            await _service.SoftDeleteAsync(descriptionId);
            return NoContent();
        }

        [HttpPatch("{descriptionId:int}/restore")]
        public async Task<IActionResult> Restore(int policyId, int descriptionId)
        {
            await _service.RestoreAsync(descriptionId);
            return NoContent();
        }
    }
}
