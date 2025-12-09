using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyEmployee;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyEmployeeService(
        IUnitOfWork unitOfWork,
        IPoliciesOnEmployeeRepo policiesOnEmployeeRepo,
        IPolicyRepo policyRepo,
        IEmpRegisterRepo empRegisterRepo,
        ILogger<PolicyEmployeeService> logger
        ) : IPolicyEmployeeService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IPoliciesOnEmployeeRepo _policiesOnEmployeeRepo = policiesOnEmployeeRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IEmpRegisterRepo _empRegisterRepo = empRegisterRepo;
        readonly ILogger<PolicyEmployeeService> _logger = logger;

        public async Task<IEnumerable<PolicyEmployeeDto>> GetActivePoliciesByEmployeeAsync(int employeeId)
        {
            try
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

        public async Task<IEnumerable<PolicyEmployeeDto>> GetPoliciesByEmployeeAsync(int employeeId)
        {
            try
            {
                var all = await _policiesOnEmployeeRepo.GetAllAsync();
                return all
                    .Where(p => p.EmployeeId == employeeId && p.IsDeleted == false)
                    .Select(p => new PolicyEmployeeDto
                    {
                        EmployeeId = p.EmployeeId,
                        PolicyId = p.PolicyId,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        Status = p.Status.ToString()
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

        public async Task<bool> IsPolicyActiveForEmployeeAsync(int employeeId, int policyId)
        {
            try
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

        public async Task<int?> AssignPolicyToEmployeeAsync(int employeeId, int policyId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var matchPolicy = await _policyRepo.GetOneAsync(policyId)
                    ?? throw new BusinessException("Policy not found.", 404);

                var matchEmployee = await _empRegisterRepo.GetOneAsync(employeeId)
                    ?? throw new BusinessException("Employee not found.", 404);

                var all = await _policiesOnEmployeeRepo.GetAllAsync();
                var exist = all.Any(p =>
                    p.EmployeeId == employeeId &&
                    p.PolicyId == policyId &&
                    p.Status == StatusOfPoliciesOnEmployee.Active &&
                    p.StartDate <= endDate &&
                    p.EndDate >= startDate &&
                    !p.IsDeleted
                );

                if (exist)
                    throw new BusinessException("Employee already has an active policy in this period.", 409);

                var newEntity = new PoliciesOnEmployee
                {
                    EmployeeId = employeeId,
                    PolicyId = policyId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = StatusOfPoliciesOnEmployee.Active
                };

                await _policiesOnEmployeeRepo.AddOneAsync(newEntity);
                await _unitOfWork.SaveChangesAsync();

                return newEntity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FAILED: AssignPolicyToEmployeeAsync");
                throw;
            }
        }

        public async Task<bool> RemovePolicyFromEmployeeAsync(int employeeId, int policyId)
        {
            try
            {
                var all = await _policiesOnEmployeeRepo.GetAllAsync();
                var matches = all.Where(p =>
                    p.EmployeeId == employeeId &&
                    p.PolicyId == policyId &&
                    !p.IsDeleted
                ).ToList();

                if (!matches.Any())
                    throw new BusinessException("No policy found for this employee.", 404);

                foreach (var match in matches)
                {
                    match.Status = StatusOfPoliciesOnEmployee.Cancelled;
                    match.IsDeleted = true;
                }

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FAILED: RemovePolicyFromEmployeeAsync");
                throw;
            }
        }

        public async Task<bool> UpdatePolicyDurationAsync(int id, DateOnly newStartDate, DateOnly newEndDate)
        {
            try
            {
                var match = await _policiesOnEmployeeRepo.GetOneAsync(id)
                    ?? throw new BusinessException("Policy employee record not found.", 404);

                if (newEndDate <= newStartDate)
                    throw new BusinessException("EndDate must be after StartDate.", 400);

                match.StartDate = newStartDate;
                match.EndDate = newEndDate;

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FAILED: UpdatePolicyDurationAsync");
                throw;
            }
        }

        Task<int?> IPolicyEmployeeService.AssignPolicyToEmployeeAsync(int employeeId, int policyId, DateOnly startDate, DateOnly endDate)
        {
            throw new NotImplementedException();
        }
    }
}
