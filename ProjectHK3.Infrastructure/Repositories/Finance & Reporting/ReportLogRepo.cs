using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Finance___Reporting
{
    public class ReportLogRepo(ApplicationDbContext context) : IReportLogRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<ReportLog?> AddOneAsync(ReportLog entity)
        {
            await _context.ReportLog.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.ReportLog.FindAsync(id);
            if (match is null) return false;
            _context.ReportLog.Remove(match);
            return true;
        }

        public async Task<IEnumerable<ReportLog>> GetAllAsync()
        {
            return await _context.ReportLog.ToListAsync();
        }

        public async Task<ReportLog?> GetOneAsync(int id)
        {
            return await _context.ReportLog.FindAsync(id);
        }

        public async Task<ReportLog?> UpdateOneAsync(ReportLog entity)
        {
            var match = await _context.ReportLog.FindAsync(entity.Id);
            if (match is null) return null;
            _context.ReportLog.Update(entity);
            return match;
        }
    }
}
