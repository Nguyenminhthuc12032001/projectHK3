using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Authentication
{
    public class PasswordResetTokenRepo(ApplicationDbContext context) : IPasswordResetTokenRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<PasswordResetToken?> AddOneAsync(PasswordResetToken entity)
        {
            await _context.PasswordResetToken.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.PasswordResetToken.FindAsync(id);
            if (match == null) return false;
            _context.PasswordResetToken.Remove(match);
            return true;
        }

        public async Task<IEnumerable<PasswordResetToken>> GetAllAsync()
        {
            return await _context.PasswordResetToken.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<PasswordResetToken?> GetOneAsync(int id)
        {
            return await _context.PasswordResetToken.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.PasswordResetToken.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match == null) return false;
            match.IsDeleted = false;
            _context.PasswordResetToken.Update(match);
            return true;
        }

        public async Task<PasswordResetToken?> UpdateOneAsync(PasswordResetToken entity)
        {
            var match = await _context.PasswordResetToken.FindAsync(entity.Id);
            if (match == null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
