using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims
{
    public class PolicyRequestDetailRepo(ApplicationDbContext context) : IPolicyRequestDetailRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<PolicyRequestDetail?> AddOneAsync(PolicyRequestDetail entity)
        {
            await _context.PolicyRequestDetail.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.PolicyRequestDetail.FindAsync(id);
            if (match is null) return false;
            _context.PolicyRequestDetail.Remove(match);
            return true;
        }

        public async Task<IEnumerable<PolicyRequestDetail>> GetAllAsync()
        {
            return await _context.PolicyRequestDetail.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<PolicyRequestDetail?> GetOneAsync(int id)
        {
            return await _context.PolicyRequestDetail.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var macth = await _context.PolicyRequestDetail.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (macth is null) return false;
            macth.IsDeleted = false;
            _context.PolicyRequestDetail.Update(macth);
            return true;
        }

        public async Task<PolicyRequestDetail?> UpdateOneAsync(PolicyRequestDetail entity)
        {
            var match = await _context.PolicyRequestDetail.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
