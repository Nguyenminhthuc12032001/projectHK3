using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyDescription;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyDescriptionService(
        IUnitOfWork uniOfWork,
        IPolicyTotalDescriptionRepo policyTotalDescriptionRepo,
        IPolicyRepo policyRepo
        ) : IPolicyDescriptionService
    {
        readonly IUnitOfWork _unitOfWork = uniOfWork;
        readonly IPolicyTotalDescriptionRepo _policyTotalDescriptionRepo = policyTotalDescriptionRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        public async Task<int?> AddDescriptionAsync(int policyId, CreatePolicyDescriptionRequest request)
        {
            var matchPolicy = await _policyRepo.GetOneAsync( policyId );
            if ( matchPolicy == null ) return null;
            PolicyTotalDescription newEntity = new()
            {
                PolicyId = policyId,
                PolicySummary = request.PolicySummary,
                Benefits = request.Benefits,
                Exclutions = request.Exclusions,
                TermsConditions = request.TermsConditions
            };

            var result = await _policyTotalDescriptionRepo.AddOneAsync(newEntity);
            return result?.Id;
        }

        public async Task<PolicyDescriptionDto?> GetByIdAsync(int descriptionId)
        {
            var match = await _policyTotalDescriptionRepo.GetOneAsync(descriptionId);
            if ( match == null ) return null;
            return new PolicyDescriptionDto
            {
                Id = descriptionId,
                PolicyId = match.Id,
                PolicySummary = match.PolicySummary,
                Benefits = match.Benefits,
                Exclusions = match.Exclutions,
                TermsConditions = match.TermsConditions
            };
        }

        public async Task<IEnumerable<PolicyDescriptionDto>> GetByPolicyAsync(int policyId)
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

        public Task<bool> RestoreAsync(int descriptionId)
        {
            return _policyTotalDescriptionRepo.ReStoreById(descriptionId);
        }

        public async Task<bool> SoftDeleteAsync(int descriptionId)
        {
            return await _policyTotalDescriptionRepo.DeleteOneAsync(descriptionId);
        }

        public async Task<bool> UpdateDescriptionAsync(int descriptionId, UpdatePolicyDescriptionRequest request)
        {
            var match = await _policyTotalDescriptionRepo.GetOneAsync(descriptionId);
            if (match == null) return false;
            match.PolicySummary = request.PolicySummary ?? match.PolicySummary;
            match.Benefits = request.Benefits ?? match.Benefits;
            match.Exclutions = request.Exclusions ?? match.Exclutions;
            match.TermsConditions = request.TermsConditions ?? match.TermsConditions;
            return await _policyTotalDescriptionRepo.UpdateOneAsync(match) != null;
        }
    }
}
