using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Insurer___Policy
{
    public class CompanyDetailRepo(ApplicationDbContext context) : ICompanyDetailRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<CompanyDetail?> AddOneAsync(CompanyDetail entity)
        {
            await _context.CompanyDetail.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.CompanyDetail.FindAsync(id);
            if (match is null) return false;
            _context.CompanyDetail.Remove(match);
            return true;
        }

        public async Task<IEnumerable<CompanyDetail>> GetAllAsync()
        {
            return await _context.CompanyDetail.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<CompanyDetail?> GetOneAsync(int id)
        {
            return await _context.CompanyDetail.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.CompanyDetail.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match is null) return false;
            match.IsDeleted = false;
            _context.CompanyDetail.Update(match);
            return true;
        }

        public async Task<CompanyDetail?> UpdateOneAsync(CompanyDetail entity)
        {
            var match = await _context.CompanyDetail.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
