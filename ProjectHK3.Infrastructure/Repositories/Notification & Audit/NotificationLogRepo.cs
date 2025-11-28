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
            throw new NotSupportedException("");
        }

        public async Task<IEnumerable<NotificationLog>> GetAllAsync()
        {
            return await _context.NotificationLog.ToListAsync();
        }

        public async Task<NotificationLog?> GetOneAsync(int id)
        {
            return await _context.NotificationLog.FindAsync(id);
        }

        public Task<NotificationLog?> UpdateOneAsync(NotificationLog entity)
        {
            throw new NotSupportedException();
        }
    }
}
