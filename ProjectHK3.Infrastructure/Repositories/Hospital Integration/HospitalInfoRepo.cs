using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Hospital_Integration
{
    public class HospitalInfoRepo(ApplicationDbContext context) : IHospitalInfoRepo
    {
        readonly ApplicationDbContext _context = context;
        public async Task<HospitalInfo?> AddOneAsync(HospitalInfo entity)
        {
            await _context.HospitalInfo.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.HospitalInfo.FindAsync(id);
            if (match is null) return false;
            _context.HospitalInfo.Remove(match);
            return true;
        }

        public async Task<IEnumerable<HospitalInfo>> GetAllAsync()
        {
            return await _context.HospitalInfo.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<HospitalInfo?> GetOneAsync(int id)
        {
            return await _context.HospitalInfo.FindAsync(id);
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.HospitalInfo.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match is null) return false;
            match.IsDeleted = false;
            _context.HospitalInfo.Update(match);
            return true;
        }

        public async Task<HospitalInfo?> UpdateOneAsync(HospitalInfo entity)
        {
            var match = await _context.HospitalInfo.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
