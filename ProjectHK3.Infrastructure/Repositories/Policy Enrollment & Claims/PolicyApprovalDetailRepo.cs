using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims
{
    public class PolicyApprovalDetailRepo(ApplicationDbContext context) : IPolicyApprovalDetailRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<PolicyApprovalDetail?> AddOneAsync(PolicyApprovalDetail entity)
        {
            await _context.PolicyApprovalDetail.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.PolicyApprovalDetail.FindAsync(id);
            if (match is null) return false;
            _context.PolicyApprovalDetail.Remove(match);
            return true;
        }

        public async Task<IEnumerable<PolicyApprovalDetail>> GetAllAsync()
        {
            return await _context.PolicyApprovalDetail.ToListAsync();
        }

        public async Task<PolicyApprovalDetail?> GetOneAsync(int id)
        {
            return await _context.PolicyApprovalDetail.FindAsync(id);
        }

        public async Task<PolicyApprovalDetail?> UpdateOneAsync(PolicyApprovalDetail entity)
        {
            var match = await _context.PolicyApprovalDetail.FindAsync(entity.Id);
            if (match is null) return null;
            return match;
        }
    }
}
