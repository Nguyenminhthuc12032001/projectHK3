using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Notification___Audit
{
    public class NotificationLogRepo(ApplicationDbContext context) : INotificationLogRepo
    {

        readonly ApplicationDbContext _context = context;
        public async Task<NotificationLog?> AddOneAsync(NotificationLog entity)
        {
            await _context.NotificationLog.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.NotificationLog.FindAsync(id);
            if (match is null) return false;
            _context.NotificationLog.Remove(match);
            return true;
        }

        public async Task<IEnumerable<NotificationLog>> GetAllAsync()
        {
            return await _context.NotificationLog.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<NotificationLog?> GetOneAsync(int id)
        {
            return await _context.NotificationLog.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.NotificationLog.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match is null) return false;
            match.IsDeleted = false;
            _context.NotificationLog.Update(match);
            return true;
        }

        public async Task<NotificationLog?> UpdateOneAsync(NotificationLog entity)
        {
            var match = await _context.NotificationLog.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
