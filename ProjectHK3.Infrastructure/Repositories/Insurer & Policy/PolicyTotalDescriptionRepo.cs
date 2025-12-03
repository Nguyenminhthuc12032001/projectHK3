using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Insurer___Policy
{
    public class PolicyTotalDescriptionRepo(ApplicationDbContext context) : IPolicyTotalDescriptionRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<PolicyTotalDescription?> AddOneAsync(PolicyTotalDescription entity)
        {
            PolicyTotalDescription newEntity = new()
            {
                PolicyId = entity.PolicyId,
                PolicySummary = entity.PolicySummary,
                Benefits = entity.Benefits,
                Exclutions = entity.Exclutions,
                TermsConditions = entity.TermsConditions,
            };
            await _context.PolicyTotalDescription.AddAsync(newEntity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.PolicyTotalDescription.FindAsync(id);
            if (match == null) return false;
            _context.PolicyTotalDescription.Remove(match);
            return true;
        }

        public async Task<IEnumerable<PolicyTotalDescription>> GetAllAsync()
        {
            return await _context.PolicyTotalDescription.ToListAsync();
        }

        public async Task<PolicyTotalDescription?> GetOneAsync(int id)
        {
            return await _context.PolicyTotalDescription.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.PolicyTotalDescription.FindAsync(id);
            if (match == null) return false;
            _context.PolicyTotalDescription.Remove(match);
            return true;
        }

        public async Task<PolicyTotalDescription?> UpdateOneAsync(PolicyTotalDescription entity)
        {
            var match = await _context.PolicyTotalDescription.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
