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
            throw new NotSupportedException("AuditTrail cannot be deleted.");
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
            throw new NotSupportedException("AuditTrail cannot be edited");
        }
    }
}
