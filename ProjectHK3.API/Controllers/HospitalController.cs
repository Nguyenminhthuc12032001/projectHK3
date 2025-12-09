using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Hospital;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Hospital;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        private readonly IMapper _mapper;
        private readonly ILogger<HospitalController> _logger;

        public HospitalController(
            IHospitalService hospitalService,
            IMapper mapper,
            ILogger<HospitalController> logger)
        {
            _hospitalService = hospitalService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHospital([FromBody] CreateHospitalModel model)
        {
            try
            {
                var dto = _mapper.Map<CreateHospital>(model);
                var resultId = await _hospitalService.CreateNewAsync(dto);

                if (resultId is null)
                    return BadRequest("Failed to create hospital.");

                return CreatedAtAction(nameof(GetHospitalById), new { id = resultId }, new { id = resultId });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllHospitals()
        {
            try
            {
                var data = await _hospitalService.GetAllAsync();
                var result = data.Select(_mapper.Map<HospitalListModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHospitalById(int id)
        {
            try
            {
                var data = await _hospitalService.GetOneByIdAsync(id);
                if (data is null) return NotFound("Hospital not found.");

                var result = _mapper.Map<HospitalModel>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHospital(int id, [FromBody] UpdateHospitalModel model)
        {
            try
            {
                var dto = _mapper.Map<UpdateHospital>(model);
                var success = await _hospitalService.UpdateByIdAsync(id, dto);

                if (!success) return NotFound("Hospital not found.");
                return NoContent();
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            try
            {
                var success = await _hospitalService.DeleteByIdAsync(id);
                if (!success) return NotFound("Hospital not found.");
                return NoContent();
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
