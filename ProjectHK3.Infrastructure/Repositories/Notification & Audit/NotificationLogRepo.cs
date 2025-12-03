using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<bool> DeleteOneAsync(int id)
        {
            throw new NotSupportedException("Notification logs cannot be deleted.");
            if (match is null) return false;
            _context.NotificationLog.Remove(match);
            return true;
        }

        public async Task<IEnumerable<NotificationLog>> GetAllAsync()
        {
            return await _context.NotificationLog.ToListAsync();
        }

        public async Task<NotificationLog?> GetOneAsync(int id)
        {
            return await _context.NotificationLog.FindAsync(id);
        }
            throw new NotSupportedException("Notification logs cannot be updated.");
        public Task<NotificationLog?> UpdateOneAsync(NotificationLog entity)
        {
            var match = await _context.NotificationLog.FindAsync(entity.Id);
            if (match is null) return null;
            _context.NotificationLog.Update(entity);
            return match;
        }
    }
}
