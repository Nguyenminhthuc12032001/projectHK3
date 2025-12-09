using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Policy;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyService(
        IPolicyRepo policyRepo,
        IUnitOfWork unitOfWork,
        ILogger<PolicyService> logger
        ) : IPolicyService
    {
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly ILogger<PolicyService> _logger = logger;
        public async Task<int> CreatePolicyAsync(CreatePolicyDto request)
        {
            try
            {
                Policy newPolicy = new()
                {
                    CompanyId = request.CompanyId,
                    PolicyName = request.PolicyName,
                    CoverageAmount = request.CoverageAmount,
                    PremiumAmount = request.PremiumAmount,
                    EffectiveFrom = request.EffectiveFrom,
                    EffectiveTo = request.EffectiveTo,
                    Status = request.Status switch
                    {
                        "Draft" => StatusOfPolicy.Draft,
                        "Active" => StatusOfPolicy.Active,
                        "Expired" => StatusOfPolicy.Expired,
                        _ => StatusOfPolicy.Draft
                    }
                };
                await _policyRepo.AddOneAsync(newPolicy);
                await _unitOfWork.SaveChangesAsync();
                return newPolicy.Id;
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

        public async Task<IEnumerable<ListPolicyDto>> GetAllPoliciesAsync(bool includeDeleted)
        {
            try
            {
                var policies = await _policyRepo.GetAllAsync();
                return [.. policies
                    .Where(p => includeDeleted || p.IsDeleted == false)
                    .Select(p => new ListPolicyDto
                    {
                        Id = p.Id,
                        PolicyName = p.PolicyName!,
                        CoverageAmount = p.CoverageAmount,
                        PremiumAmount = p.PremiumAmount
                    })];
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

        public async Task<DetailPolicyDto?> GetPolicyByIdAsync(int policyId)
        {
            try
            {
                var match = await _policyRepo.GetOneAsync(policyId) ?? throw new BusinessException("Policy not found", 404);
                return new DetailPolicyDto
                {
                    Id = match.Id,
                    PolicyName = match.PolicyName!,
                    CoverageAmount = match.CoverageAmount,
                    PremiumAmount = match.PremiumAmount,
                    EffectiveFrom = match.EffectiveFrom,
                    EffectiveTo = match.EffectiveTo,
                    Status = match.Status.ToString(),
                    CreatedAt = match.CreatedAt,
                    UpdatedAt = match.UpdatedAt,
                    CreatedBy = match.CreatedBy!,
                    UpdatedBy = match.UpdatedBy!
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

        public async Task<bool> RestorePolicyAsync(int policyId)
        {
            try
            {
                await _policyRepo.ReStoreById(policyId);
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

        public Task<IEnumerable<ListPolicyDto>> SearchPoliciesAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeletePolicyAsync(int policyId)
        {
            try
            {
                var match = await _policyRepo.DeleteOneAsync(policyId);
                await _unitOfWork.SaveChangesAsync();
                return match;
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

        public async Task<bool> UpdatePolicyAsync(int policyId, UpdatePolicyDto request)
        {
            try
            {
                var match = await _policyRepo.GetOneAsync(policyId) ?? throw new BusinessException("Policy not found", 404);
                match.PolicyName = request.PolicyName ?? match.PolicyName;
                match.CoverageAmount = request.CoverageAmount ?? match.CoverageAmount;
                match.PremiumAmount = request.PremiumAmount ?? match.PremiumAmount;
                match.EffectiveTo = request.EffectiveTo ?? match.EffectiveTo;
                if (!string.IsNullOrWhiteSpace(request.Status))
                {
                    match.Status = request.Status.ToLower() switch
                    {
                        "draft" => StatusOfPolicy.Draft,
                        "active" => StatusOfPolicy.Active,
                        "expired" => StatusOfPolicy.Expired,
                        _ => match.Status
                    };
                }
                await _policyRepo.UpdateOneAsync(match);
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
