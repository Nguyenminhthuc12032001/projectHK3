using ProjectHK3.Application.DTOs.PolicyEmployee;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IPolicyEmployeeService
    {
        Task<int> AssignPolicyToEmployeeAsync(int employeeId, int policyId, DateOnly startDate, DateOnly endDate);
        Task<bool> RemovePolicyFromEmployeeAsync(int employeeId, int policyId);
        Task<IEnumerable<PolicyEmployeeDto>> GetPoliciesByEmployeeAsync(int employeeId);
        Task<IEnumerable<PolicyEmployeeDto>> GetActivePoliciesByEmployeeAsync(int employeeId);
        Task<bool> UpdatePolicyDurationAsync(int employeeId, int policyId, DateOnly newStartDate, DateOnly newEndDate);
        Task<bool> IsPolicyActiveForEmployeeAsync(int employeeId, int policyId);
    }
}
