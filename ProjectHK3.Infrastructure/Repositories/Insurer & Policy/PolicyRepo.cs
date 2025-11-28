using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Insurer___Policy
{
    public class PolicyRepo(ApplicationDbContext context) : IPolicyRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<Policy?> AddOneAsync(Policy entity)
        {
            await _context.Policy.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.Policy.FindAsync(id);
            if (match is null) return false;
            _context.Policy.Remove(match);
            return true;
        }

        public async Task<IEnumerable<Policy>> GetAllAsync()
        {
            return await _context.Policy.ToListAsync();
        }

        public async Task<Policy?> GetOneAsync(int id)
        {
            return await _context.Policy.FindAsync(id);
        }

        public async Task<Policy?> UpdateOneAsync(Policy entity)
        {
            var match = await _context.Policy.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Policy.Update(entity);
            return match;
        }
    }
}
