using ProjectHK3.Application.DTOs.Policy;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IPolicyService
    {
        Task<int> CreatePolicyAsync(CreatePolicyDto request);
        Task<bool> UpdatePolicyAsync(int policyId, UpdatePolicyDto request);
        Task<bool> SoftDeletePolicyAsync(int policyId);
        Task<bool> RestorePolicyAsync(int policyId);
        Task<DetailPolicyDto?> GetPolicyByIdAsync(int policyId);
        Task<IEnumerable<ListPolicyDto>> GetAllPoliciesAsync(bool includeDeleted = false);
        Task<IEnumerable<ListPolicyDto>> SearchPoliciesAsync(string query);
    }
}
