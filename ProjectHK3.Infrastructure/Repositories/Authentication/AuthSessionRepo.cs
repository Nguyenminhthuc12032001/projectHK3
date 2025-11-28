using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Authentication
{
    public class AuthSessionRepo(ApplicationDbContext context) : IAuthSessionRepo
    {
        readonly ApplicationDbContext _context = context;

        public async Task<AuthSession?> AddOneAsync(AuthSession entity)
        {
            await _context.AuthSession.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.AuthSession.FindAsync(id);
            if (match == null) return false;
            _context.AuthSession.Remove(match);
            return true;
        }

        public async Task<IEnumerable<AuthSession>> GetAllAsync()
        {
            return await _context.AuthSession.ToListAsync();
        }

        public async Task<AuthSession?> GetOneAsync(int id)
        {
            return await _context.AuthSession.FindAsync(id);
        }

        public async Task<AuthSession?> UpdateOneAsync(AuthSession entity)
        {
            var match = await _context.AuthSession.FindAsync(entity.Id);
            if (match == null) return null;
            _context.AuthSession.Update(entity);
            return match;
        }
    }
}
