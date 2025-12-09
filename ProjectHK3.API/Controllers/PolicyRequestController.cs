using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.PolicyRequest;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyRequest;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyRequestController : ControllerBase
    {
        private readonly IPolicyRequestService _policyRequestService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePolicyRequestModel> _createValidator;
        private readonly IValidator<UpdatePolicyRequestModel> _updateValidator;
        private readonly IValidator<PolicyRequestDocumentModel> _documentValidator;

        public PolicyRequestController(
            IPolicyRequestService policyRequestService,
            IMapper mapper,
            IValidator<CreatePolicyRequestModel> createValidator,
            IValidator<UpdatePolicyRequestModel> updateValidator,
            IValidator<PolicyRequestDocumentModel> documentValidator)
        {
            _policyRequestService = policyRequestService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _documentValidator = documentValidator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _policyRequestService.GetByIdAsync(id)
                ?? throw new BusinessException("Request not found", 404);

            return Ok(result);
        }

        [Authorize(Roles = "Employee,Admin")]
        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {
            var result = await _policyRequestService.GetRequestByEmployeeId(employeeId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var result = await _policyRequestService.GetPendingRequestsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> SubmitRequest([FromBody] CreatePolicyRequestModel model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BusinessException(validation.ToString(), 400);

            var request = _mapper.Map<CreatePolicyRequestDto>(model);
            var id = await _policyRequestService.SubmitRequestAsync(request)
                ?? throw new BusinessException("Employee already has a pending request", 409);

            return Ok(new { message = "Request submitted successfully", id });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] UpdatePolicyRequestModel model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BusinessException(validation.ToString(), 400);

            var dto = _mapper.Map<UpdatePolicyRequestDto>(model);

            var success = await _policyRequestService.UpdateRequestAsync(id, dto);
            if(!success) throw new BusinessException("Error while updating request", 400);

            return Ok(new { message = "Request updated successfully" });
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{id:int}/documents")]
        public async Task<IActionResult> UpdateDocuments(int id, [FromBody] IEnumerable<PolicyRequestDocumentModel> model)
        {
            foreach (var doc in model)
            {
                var validation = await _documentValidator.ValidateAsync(doc);
                if (!validation.IsValid)
                    throw new BusinessException(validation.ToString(), 400);
            }

            var dto = _mapper.Map<IEnumerable<PolicyRequestDocumentDto>>(model);

            var success = await _policyRequestService.UpdateDocumentsAsync(id, dto);
            if (!success)
                throw new BusinessException("Documents update failed", 400);

            return Ok(new { message = "Documents updated successfully" });
        }
    }
}
