using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<bool> DeleteOneAsync(int id)
        {
            throw new NotSupportedException("Audit trails cannot be deleted.");
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            return true;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            return true;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            return true;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            return true;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            var match = await _context.AuditTrail.FindAsync(entity.Id);
            if (match is null) return null;
            _context.AuditTrail.Update(entity);
            return match;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            var match = await _context.AuditTrail.FindAsync(entity.Id);
            if (match is null) return null;
            _context.AuditTrail.Update(entity);
            return match;
            if (match is null) return false;
            _context.AuditTrail.Remove(match);
            return true;
        }

        public async Task<IEnumerable<AuditTrail>> GetAllAsync()
        {
            return  await _context.AuditTrail.ToListAsync();
        }

        public async Task<AuditTrail?> GetOneAsync(int id)
        {
            return await _context.AuditTrail.FindAsync(id);
        }

        public Task<AuditTrail?> UpdateOneAsync(AuditTrail entity)
        {
            var match = await _context.AuditTrail.FindAsync(entity.Id);
            if (match is null) return null;
            _context.AuditTrail.Update(entity);
            return match;
        }
    }
}
