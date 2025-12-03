using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyApproval;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyApprovalService(
        IPolicyApprovalDetailRepo policyApprovalDetailRepo,
        IPolicyRequestDetailRepo policyRequestDetailRepo,
        IUnitOfWork unitOfWork
        ) : IPolicyApprovalService
    {
        readonly IPolicyApprovalDetailRepo _policyApprovalDetailRepo = policyApprovalDetailRepo;
        readonly IPolicyRequestDetailRepo _policyRequestDetailRepo = policyRequestDetailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> ApprovePolicyAsync(int policyRequestId, int adminId, string remarks)
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
        public async Task<IEnumerable<PolicyApprovalDetailDto>> GetApprovalHistoryAsync(int policyRequestId)
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

        public async Task<PolicyApprovalDetailDto?> GetLastestApprovalAsync(int policyRequestId)
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

        public async Task<IEnumerable<PolicyRequestListDto>> GetPendingApprovalAsync()
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

        public async Task<bool> RejectPolicyAsync(int policyRequestId, int adminId, string remarks)
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
    }
}
