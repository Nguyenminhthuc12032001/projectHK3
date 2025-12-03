using ProjectHK3.Application.DTOs.PolicyApproval;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IPolicyApprovalService
    {
        Task<bool> ApprovePolicyAsync(int policyRequestId, int adminId, string remarks);
        Task<bool> RejectPolicyAsync(int policyRequestId, int adminId, string remarks);
        Task<IEnumerable<PolicyApprovalDetailDto>> GetApprovalHistoryAsync(int policyRequestId);
        Task<PolicyApprovalDetailDto?> GetLastestApprovalAsync(int policyRequestId);
        Task<IEnumerable<PolicyRequestListDto>> GetPendingApprovalAsync();
    }
}
