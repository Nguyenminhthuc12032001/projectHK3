using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Authentication
{
    public class EmpRegisterRepo(ApplicationDbContext context) : IEmpRegisterRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<EmpRegister?> AddOneAsync(EmpRegister entity)
        {
            await _context.EmpRegister.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.EmpRegister.FindAsync(id);
            if (match is null) return false;
            _context.EmpRegister.Remove(match);
            return true;
        }

        public async Task<IEnumerable<EmpRegister>> GetAllAsync()
        {
            return await _context.EmpRegister.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<EmpRegister?> GetOneAsync(int id)
        {
            return await _context.EmpRegister.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.EmpRegister.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match == null) return false;
            match.IsDeleted = false;
            _context.EmpRegister.Update(match);
            return true;
        }

        public async Task<EmpRegister?> UpdateOneAsync(EmpRegister entity)
        {
            var match = await _context.EmpRegister.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
