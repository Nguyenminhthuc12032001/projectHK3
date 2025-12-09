using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHK3.Api.Models.Notification;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Notification;
using ProjectHK3.Application.Exceptions;

namespace ProjectHK3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(
            INotificationService notificationService,
            IMapper mapper,
            ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationModel model)
        {
            try
            {
                var dto = _mapper.Map<SendNotificationRequest>(model);
                var newId = await _notificationService.SendNotificationAsync(dto);
                if (newId == 0)
                    return BadRequest("Failed to send notification.");

                return Ok(new { id = newId });
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

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationService.GetNotificationByUserASync(userId);
                var result = notifications.Select(_mapper.Map<NotificationModel>);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("unread-count/{userId:int}")]
        public async Task<IActionResult> CountUnread(int userId)
        {
            try
            {
                var count = await _notificationService.CountUnreadNotificationAsync(userId);
                return Ok(new { unread = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("read/{notificationId:int}")]
        public async Task<IActionResult> MarkRead(int notificationId)
        {
            try
            {
                var success = await _notificationService.MarkAsReadAsync(notificationId);
                if (!success) return NotFound("Notification not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("read-all/{userId:int}")]
        public async Task<IActionResult> MarkAllRead(int userId)
        {
            try
            {
                await _notificationService.MarkAllAsReadAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{notificationId:int}")]
        public async Task<IActionResult> SoftDelete(int notificationId)
        {
            try
            {
                var result = await _notificationService.SoftDeleteAsync(notificationId);
                if (!result) return NotFound("Notification not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("restore/{notificationId:int}")]
        public async Task<IActionResult> Restore(int notificationId)
        {
            try
            {
                var success = await _notificationService.RestoreAsync(notificationId);
                if (!success) return BadRequest("Notification not available for restore.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
