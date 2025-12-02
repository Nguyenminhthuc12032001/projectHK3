using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Policy;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyService(
        IPolicyRepo policyRepo,
        IUnitOfWork unitOfWork
        ) : IPolicyService
    {
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<int> CreatePolicyAsync(CreatePolicyDto request)
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

        public async Task<IEnumerable<ListPolicyDto>> GetAllPoliciesAsync(bool includeDeleted)
        {
            var policies = await _policyRepo.GetAllAsync();
            return policies
                .Where(p => includeDeleted || p.IsDeleted == false)
                .Select(p => new ListPolicyDto
                {
                    Id = p.Id,
                    PolicyName = p.PolicyName!,
                    CoverageAmount = p.CoverageAmount,
                    PremiumAmount = p.PremiumAmount
                }).ToList();
        }

        public async Task<DetailPolicyDto?> GetPolicyByIdAsync(int policyId)
        {
            var match = await _policyRepo.GetOneAsync(policyId);
            if (match == null) return null;
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

        public async Task<bool> RestorePolicyAsync(int policyId)
        {
            await _policyRepo.ReStoreById(policyId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<ListPolicyDto>> SearchPoliciesAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeletePolicyAsync(int policyId)
        {
            var match = await _policyRepo.DeleteOneAsync(policyId);
            await _unitOfWork.SaveChangesAsync();
            return match;
        }

        public async Task<bool> UpdatePolicyAsync(int policyId, UpdatePolicyDto request)
        {
            var match = await _policyRepo.GetOneAsync(policyId);
            if (match == null) return false;
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
    }
}
