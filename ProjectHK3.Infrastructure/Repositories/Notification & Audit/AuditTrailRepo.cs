using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Notification___Audit
{
    public class AuditTrailRepo(ApplicationDbContext context) : IAuditTrailRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<AuditTrail?> AddOneAsync(AuditTrail entity)
        {
            await _context.AuditTrail.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            throw new NotSupportedException("Audit trails cannot be deleted.");
        }

        public async Task<IEnumerable<AuditTrail>> GetAllAsync()
        {
            return await _context.AuditTrail.ToListAsync();
        }

        public async Task<AuditTrail?> GetOneAsync(int id)
        {
            return await _context.AuditTrail.FindAsync(id);
        }

        public async Task<AuditTrail?> UpdateOneAsync(AuditTrail entity)
        {
            throw new NotSupportedException("Audit trails cannot be updated.");
        }
    }
}
