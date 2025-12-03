using ProjectHK3.Application.DTOs.PolicyDescription;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IPolicyDescriptionService
    {
        Task<int> AddDescriptionAsync(int policyId, CreatePolicyDescriptionRequest request);
        Task<bool> UpdateDescriptionAsync(int descriptionId, UpdatePolicyDescriptionRequest request);
        Task<bool> SoftDeleteAsync(int descriptionId);
        Task<bool> RestoreAsync(int descriptionId);
        Task<PolicyDescriptionDto?> GetByIdAsync(int descriptionId);
        Task<IEnumerable<PolicyDescriptionDto>> GetByPolicyAsync(int policyId);
    }
}
