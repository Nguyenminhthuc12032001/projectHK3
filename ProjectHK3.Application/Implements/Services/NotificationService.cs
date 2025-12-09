using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Notification;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class NotificationService(
        INotificationLogRepo notificationLogRepo,
        IUnitOfWork unitOfWork,
        ILogger<NotificationService> logger
        ) : INotificationService
    {
        readonly INotificationLogRepo _notificationLogRepo = notificationLogRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly ILogger<NotificationService> _logger = logger;

        public async Task<int> CountUnreadNotificationAsync(int userId)
        {
            try
            {
                var notifications = await _notificationLogRepo.GetAllAsync();

                return notifications.Count(n =>
                    !n.IsRead &&
                    !n.IsDeleted &&
                    n.Recipient == userId.ToString()
                );
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationByUserASync(int userId)
        {
            try
            {
                var notifications = await _notificationLogRepo.GetAllAsync();

                var userNotifications = notifications.Where(n =>
                    n.Recipient == userId.ToString() &&
                    !n.IsDeleted
                ).OrderByDescending(n => n.CreatedAt);

                return userNotifications.Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Recipient = n.Recipient,
                    Type = n.Type.ToString(),
                    Subject = n.Subject,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    Status = n.Status.ToString(),
                    CreatedAt = n.CreatedAt
                });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }
        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            try
            {
                var notifications = await _notificationLogRepo.GetAllAsync();

                var userNotifications = notifications.Where(n =>
                    n.Recipient == userId.ToString() &&
                    !n.IsRead &&
                    !n.IsDeleted
                );

                foreach (var notification in userNotifications)
                {
                    notification.IsRead = true;
                    await _notificationLogRepo.UpdateOneAsync(notification);
                }

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            try
            {
                var notification = await _notificationLogRepo.GetOneAsync(notificationId);
                if (notification == null || notification.IsDeleted) return false;

                notification.IsRead = true;
                await _notificationLogRepo.UpdateOneAsync(notification);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> RestoreAsync(int notificationId)
        {
            try
            {
                var notification = await _notificationLogRepo.GetOneAsync(notificationId);
                if (notification == null || !notification.IsDeleted) return false;

                notification.IsDeleted = false;
                await _notificationLogRepo.UpdateOneAsync(notification);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<int> SendNotificationAsync(SendNotificationRequest request)
        {
            try
            {
                var notification = new NotificationLog
                {
                    Recipient = request.Recipient,
                    Type = request.Type,
                    Subject = request.Subject,
                    Message = request.Message,
                    IsRead = false,
                    Status = StatusOfNotificationLog.Queue
                };

                var result = await _notificationLogRepo.AddOneAsync(notification);
                await _unitOfWork.SaveChangesAsync();

                if (result != null)
                {
                    result.Status = StatusOfNotificationLog.Sent;
                    await _notificationLogRepo.UpdateOneAsync(result);
                    await _unitOfWork.SaveChangesAsync();
                    return result.Id;
                }

                return 0;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(int notificationId)
        {
            try
            {
                var result = await _notificationLogRepo.DeleteOneAsync(notificationId);
                await _unitOfWork.SaveChangesAsync();

                return result;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

    }
}
