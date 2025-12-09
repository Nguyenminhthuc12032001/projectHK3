using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyDescription;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyDescriptionService(
        IUnitOfWork uniOfWork,
        IPolicyTotalDescriptionRepo policyTotalDescriptionRepo,
        IPolicyRepo policyRepo,
        ILogger<PolicyDescriptionService> logger
        ) : IPolicyDescriptionService
    {
        readonly IUnitOfWork _unitOfWork = uniOfWork;
        readonly IPolicyTotalDescriptionRepo _policyTotalDescriptionRepo = policyTotalDescriptionRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly ILogger<PolicyDescriptionService> _logger = logger;
        public async Task<int?> AddDescriptionAsync(int policyId, CreatePolicyDescriptionRequest request)
        {
            try
            {
                var matchPolicy = await _policyRepo.GetOneAsync(policyId) ?? throw new BusinessException("Policy not found.", 404);
                PolicyTotalDescription newEntity = new()
                {
                    PolicyId = policyId,
                    PolicySummary = request.PolicySummary,
                    Benefits = request.Benefits,
                    Exclutions = request.Exclusions,
                    TermsConditions = request.TermsConditions
                };

                var result = await _policyTotalDescriptionRepo.AddOneAsync(newEntity);
                await _unitOfWork.SaveChangesAsync();
                return result?.Id;
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

        public async Task<PolicyDescriptionDto?> GetByIdAsync(int descriptionId)
        {
            try
            {
                var match = await _policyTotalDescriptionRepo.GetOneAsync(descriptionId) ?? throw new BusinessException("Policy Total Description not found", 404);
                return new PolicyDescriptionDto
                {
                    Id = descriptionId,
                    PolicyId = match.PolicyId,
                    PolicySummary = match.PolicySummary,
                    Benefits = match.Benefits,
                    Exclusions = match.Exclutions,
                    TermsConditions = match.TermsConditions
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

        public async Task<IEnumerable<PolicyDescriptionDto>> GetByPolicyAsync(int policyId)
        {
            try
            {
                var all = await _policyTotalDescriptionRepo.GetAllAsync();
                var matchs = all.Where(p => p.PolicyId == policyId && p.IsDeleted == false)
                    .Select(p => new PolicyDescriptionDto
                    {
                        Id = p.Id,
                        PolicyId = p.PolicyId,
                        PolicySummary = p.PolicySummary,
                        Benefits = p.Benefits,
                        Exclusions = p.Exclutions,
                        TermsConditions = p.TermsConditions
                    });
                return matchs;
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

        public async Task<bool> RestoreAsync(int descriptionId)
        {
            try
            {
                var result = await _policyTotalDescriptionRepo.ReStoreById(descriptionId);
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

        public async Task<bool> SoftDeleteAsync(int descriptionId)
        {
            try
            {
                var result = await _policyTotalDescriptionRepo.DeleteOneAsync(descriptionId);
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

        public async Task<bool> UpdateDescriptionAsync(int descriptionId, UpdatePolicyDescriptionRequest request)
        {
            try
            {
                var match = await _policyTotalDescriptionRepo.GetOneAsync(descriptionId) ?? throw new BusinessException("Policy Total Description not found", 404);
                match.PolicySummary = request.PolicySummary ?? match.PolicySummary;
                match.Benefits = request.Benefits ?? match.Benefits;
                match.Exclutions = request.Exclusions ?? match.Exclutions;
                match.TermsConditions = request.TermsConditions ?? match.TermsConditions;
                var result = await _policyTotalDescriptionRepo.UpdateOneAsync(match) != null;
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
