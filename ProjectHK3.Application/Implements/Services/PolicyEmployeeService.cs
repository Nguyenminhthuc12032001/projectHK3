using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyEmployee;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyEmployeeService(
        IUnitOfWork  unitOfWork,
        IPoliciesOnEmployeeRepo policiesOnEmployeeRepo,
        IPolicyRepo policyRepo,
        IEmpRegisterRepo empRegisterRepo
        ) : IPolicyEmployeeService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IPoliciesOnEmployeeRepo _policiesOnEmployeeRepo = policiesOnEmployeeRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IEmpRegisterRepo _empRegisterRepo = empRegisterRepo;
        public async Task<int?> AssignPolicyToEmployeeAsync(int employeeId, int policyId, DateOnly startDate, DateOnly endDate)
        {
            var matchPolicy = await _policyRepo.GetOneAsync(policyId);
            if (matchPolicy == null) return null;
            var matchEmployee = await _empRegisterRepo.GetOneAsync(employeeId);
            if (matchEmployee == null) return null;

            var all = await _policiesOnEmployeeRepo.GetAllAsync();
            var exist = all.Any(
                p =>
                p.EmployeeId == employeeId &&
                p.PolicyId == policyId &&
                p.Status == StatusOfPoliciesOnEmployee.Active &&
                p.StartDate <= endDate &&
                p.EndDate >= startDate &&
                p.IsDeleted == false
                );
            if (exist) return null;

            PoliciesOnEmployee newEntity = new()
            {
                EmployeeId = employeeId,
                PolicyId = policyId,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await _policiesOnEmployeeRepo.AddOneAsync(newEntity);
            await _unitOfWork.SaveChangesAsync();
            return result?.Id;
        }

        public async Task<IEnumerable<PolicyEmployeeDto>> GetActivePoliciesByEmployeeAsync(int employeeId)
        {
            var all = await _policiesOnEmployeeRepo.GetAllAsync();
            return all
                .Where(p => p.EmployeeId == employeeId && p.Status == StatusOfPoliciesOnEmployee.Active && p.IsDeleted == false)
                .Select(p => new PolicyEmployeeDto
                {
                    EmployeeId = p.EmployeeId,
                    PolicyId = p.PolicyId,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status.ToString()
                });
        }

        public async Task<IEnumerable<PolicyEmployeeDto>> GetPoliciesByEmployeeAsync(int employeeId)
        {
            var all = await _policiesOnEmployeeRepo.GetAllAsync();
            return all
                .Where(p => p.EmployeeId == employeeId  && p.IsDeleted == false)
                .Select(p => new PolicyEmployeeDto
                {
                    EmployeeId = p.EmployeeId,
                    PolicyId = p.PolicyId,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status.ToString()
                });
        }

        public async Task<bool> IsPolicyActiveForEmployeeAsync(int employeeId, int policyId)
        {
            var all = await _policiesOnEmployeeRepo.GetAllAsync();
            var today = DateOnly.FromDateTime(DateTime.Now);
            return all.Any(p =>
                p.EmployeeId == employeeId &&
                p.PolicyId == policyId &&
                p.Status == StatusOfPoliciesOnEmployee.Active &&
                p.StartDate <= today &&
                p.EndDate >= today &&
                p.IsDeleted == false
            );
        }

        public async Task<bool> RemovePolicyFromEmployeeAsync(int employeeId, int policyId)
        {
            var all = await _policiesOnEmployeeRepo.GetAllAsync();
            var matchs = all.Where(p =>
                p.EmployeeId == employeeId &&
                p.PolicyId == policyId &&
                p.IsDeleted == false
                );
            foreach ( var match in matchs )
            {
                match.Status = StatusOfPoliciesOnEmployee.Cancelled;
                match.IsDeleted = true;
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePolicyDurationAsync(int Id, DateOnly newStartDate, DateOnly newEndDate)
        {
            var match = await _policiesOnEmployeeRepo.GetOneAsync(Id);
            if (match is null) return false;
            match.StartDate = newStartDate;
            match.EndDate = newEndDate;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
