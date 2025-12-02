using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims
{
    public class PolicyRequestDocumentRepo(ApplicationDbContext context) : IPolicyRequestDocumentRepo
    {
        readonly ApplicationDbContext _context = context;

        public async Task<PolicyRequestDocument?> AddOneAsync(PolicyRequestDocument entity)
        {
            await _context.PolicyRequestDocument.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.PolicyRequestDocument.FindAsync(id);
            if (match == null) return false;
            _context.PolicyRequestDocument.Remove(match);
            return true;
        }

        public async Task<IEnumerable<PolicyRequestDocument>> GetAllAsync()
        {
            return await _context.PolicyRequestDocument.ToListAsync();
        }

        public async Task<PolicyRequestDocument?> GetOneAsync(int id)
        {
            return await _context.PolicyRequestDocument.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.PolicyRequestDocument.FindAsync(id);
            if (match is null) return false;
            _context.PolicyRequestDocument.Remove(match);
            return true;
        }

        public async Task<PolicyRequestDocument?> UpdateOneAsync(PolicyRequestDocument entity)
        {
            var match = await _context.PolicyRequestDocument.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return entity;
        }
    }
}
