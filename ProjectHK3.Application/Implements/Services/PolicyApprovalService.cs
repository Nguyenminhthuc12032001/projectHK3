using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyApproval;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyApprovalService(
        IPolicyApprovalDetailRepo policyApprovalDetailRepo,
        IPolicyRequestDetailRepo policyRequestDetailRepo,
        IUnitOfWork unitOfWork,
        ILogger<PolicyApprovalService> logger
        ) : IPolicyApprovalService
    {
        readonly IPolicyApprovalDetailRepo _policyApprovalDetailRepo = policyApprovalDetailRepo;
        readonly IPolicyRequestDetailRepo _policyRequestDetailRepo = policyRequestDetailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<PolicyApprovalService> _logger = logger;

        public async Task<bool> ApprovePolicyAsync(int policyRequestId, int adminId, string remarks)
        {
            try
            {
                var request = await _policyRequestDetailRepo.GetOneAsync(policyRequestId);
                if (request == null || request.IsDeleted) return false;
                if (request.Status != StatusOfPolicyRequestDetail.Pending) return false;


                request.Status = StatusOfPolicyRequestDetail.Approved;
                await _policyRequestDetailRepo.UpdateOneAsync(request);

                var approval = new PolicyApprovalDetail
                {
                    RequestId = policyRequestId,
                    AdminId = adminId,
                    ApprovalDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Remarks = remarks,
                    Status = StatusOfPolicyApprovalDetail.Approved,
                    PaymentStatus = PaymentStatusOfPolicyApprovalDetail.Unpaid
                };

                await _policyApprovalDetailRepo.AddOneAsync(approval);
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
        public async Task<IEnumerable<PolicyApprovalDetailDto>> GetApprovalHistoryAsync(int policyRequestId)
        {
            try
            {
                var approvals = await _policyApprovalDetailRepo.GetAllAsync();

                var requestApprovals = approvals.Where(a =>
                    a.RequestId == policyRequestId &&
                    !a.IsDeleted
                ).OrderByDescending(a => a.ApprovalDate);

                return requestApprovals.Select(a => new PolicyApprovalDetailDto
                {
                    Id = a.Id,
                    RequestId = a.RequestId,
                    AdminId = a.AdminId,
                    ApprovalDate = a.ApprovalDate,
                    Remarks = a.Remarks,
                    Status = a.Status.ToString(),
                    PaymentStatus = a.PaymentStatus.ToString(),
                    CreatedAt = a.CreatedAt
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

        public async Task<PolicyApprovalDetailDto?> GetLastestApprovalAsync(int policyRequestId)
        {
            try
            {
                var approvals = await _policyApprovalDetailRepo.GetAllAsync();

                var latestApproval = approvals
                    .Where(a => a.RequestId == policyRequestId && !a.IsDeleted)
                    .OrderByDescending(a => a.ApprovalDate)
                    .FirstOrDefault();

                if (latestApproval == null) return null;

                return new PolicyApprovalDetailDto
                {
                    Id = latestApproval.Id,
                    RequestId = latestApproval.RequestId,
                    AdminId = latestApproval.AdminId,
                    ApprovalDate = latestApproval.ApprovalDate,
                    Remarks = latestApproval.Remarks,
                    Status = latestApproval.Status.ToString(),
                    PaymentStatus = latestApproval.PaymentStatus.ToString(),
                    CreatedAt = latestApproval.CreatedAt
                };

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

        public async Task<IEnumerable<PolicyRequestListDto>> GetPendingApprovalAsync()
        {
            try
            {
                var requests = await _policyRequestDetailRepo.GetAllAsync();

                var pending = requests.Where(r =>
                    r.Status == StatusOfPolicyRequestDetail.Pending &&
                    !r.IsDeleted
                ).OrderBy(r => r.RequestDate);

                return pending.Select(r => new PolicyRequestListDto
                {
                    Id = r.Id,
                    EmployeeId = r.EmployeeId,
                    PolicyId = r.PolicyId,
                    RequestType = r.RequestType.ToString(),
                    RequestDate = r.RequestDate,
                    Status = r.Status.ToString(),
                    Remarks = r.Remarks
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

        public async Task<bool> RejectPolicyAsync(int policyRequestId, int adminId, string remarks)
        {
            try
            {
                var request = await _policyRequestDetailRepo.GetOneAsync(policyRequestId);
                if (request == null || request.IsDeleted) return false;
                if (request.Status != StatusOfPolicyRequestDetail.Pending) return false;

                request.Status = StatusOfPolicyRequestDetail.Rejected;
                await _policyRequestDetailRepo.UpdateOneAsync(request);

                var approval = new PolicyApprovalDetail
                {
                    RequestId = policyRequestId,
                    AdminId = adminId,
                    ApprovalDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Remarks = remarks,
                    Status = StatusOfPolicyApprovalDetail.Rejected,
                    PaymentStatus = PaymentStatusOfPolicyApprovalDetail.Unpaid
                };

                await _policyApprovalDetailRepo.AddOneAsync(approval);
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
    }
}
