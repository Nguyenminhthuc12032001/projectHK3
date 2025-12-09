using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Insurer;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Insurer;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsurerController : ControllerBase
    {
        private readonly IInsurerService _insurerService;
        private readonly IMapper _mapper;
        private readonly ILogger<InsurerController> _logger;

        public InsurerController(
            IInsurerService insurerService,
            IMapper mapper,
            ILogger<InsurerController> logger)
        {
            _insurerService = insurerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInsurer([FromBody] CreateInsurerModel model)
        {
            var dto = _mapper.Map<CreateInsurerRequest>(model);
            dto.CreateBy = 1;

            var resultId = await _insurerService.CreateInsurerAsync(dto);
            return CreatedAtAction(nameof(GetInsurerById), new { insurerId = resultId }, resultId);
        }

        [HttpGet("{insurerId:int}")]
        public async Task<IActionResult> GetInsurerById(int insurerId)
        {
            var insurer = await _insurerService.GetInsurerByIdAsync(insurerId);
            return Ok(_mapper.Map<InsurerModel>(insurer));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
        {
            var insurers = await _insurerService.GetAllInsurersAsync(includeDeleted);
            return Ok(_mapper.Map<IEnumerable<InsurerModel>>(insurers));
        }

        [HttpPut("{insurerId:int}")]
        public async Task<IActionResult> UpdateInsurer(int insurerId, [FromBody] UpdateInsurerModel model)
        {
            if (insurerId != model.Id)
                return BadRequest("Mismatched Insurer ID.");

            var dto = new UpdateInsurerRequest
            {
                CompanyName = model.CompanyName,
                Address = model.Address,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone,
                Website = model.Website
            };

            var success = await _insurerService.UpdateInsurerAsync(insurerId, dto);
            return success ? Ok("Updated successfully.") : BadRequest("Update failed.");
        }

        [HttpDelete("soft/{insurerId:int}")]
        public async Task<IActionResult> SoftDelete(int insurerId)
        {
            var success = await _insurerService.SoftDeleteAsync(insurerId);
            return success ? Ok("Soft deleted.") : NotFound();
        }

        [HttpPatch("restore/{insurerId:int}")]
        public async Task<IActionResult> Restore(int insurerId)
        {
            var success = await _insurerService.RestoreAsync(insurerId);
            return success ? Ok("Restored successfully.") : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return BadRequest("Search query required.");

            var results = await _insurerService.SearchInsurerAsync(q);
            return Ok(_mapper.Map<IEnumerable<InsurerModel>>(results));
        }
    }
}
