using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Authentication
{
    public class AdminLoginRepo(ApplicationDbContext context) : IAdminLoginRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<AdminLogin?> AddOneAsync(AdminLogin entity)
        {
            await _context.AdminLogin.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.AdminLogin.FindAsync(id);
            if (match == null) return false;

            _context.AdminLogin.Remove(match);
            return true;
        }

        public async Task<IEnumerable<AdminLogin>> GetAllAsync()
        {
            return await _context.AdminLogin.ToListAsync();
        }

        public async Task<AdminLogin?> GetOneAsync(int id)
        {
            return await _context.AdminLogin.FindAsync(id);
        }

        public async Task<AdminLogin?> UpdateOneAsync(AdminLogin entity)
        {
            var match = await _context.AdminLogin.FindAsync(entity.Id);
            if (match == null) return null;
            _context.AdminLogin.Update(entity);
            return match;
        }
    }
}
