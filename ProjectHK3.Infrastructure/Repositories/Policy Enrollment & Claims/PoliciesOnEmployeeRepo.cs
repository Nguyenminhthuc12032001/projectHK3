using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Infrastructure.Repositories.Policy_Enrollment___Claims
{
    public class PoliciesOnEmployeeRepo(ApplicationDbContext context) : IPoliciesOnEmployeeRepo
    {
        {
        }

        {
        }

        {
            return await _context.PoliciesOnEmployee.IgnoreQueryFilters().ToListAsync();
        }

        {
        }

        public async Task<bool> ReStoreById(int id)
        {
            var match = await _context.PoliciesOnEmployee.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (match is null) return false;
            match.IsDeleted = false;
            _context.PoliciesOnEmployee.Update(match);
            return true;
        }

        public async Task<PoliciesOnEmployee?> UpdateOneAsync(PoliciesOnEmployee entity)
        {
            var match = await _context.PoliciesOnEmployee.FindAsync(entity.Id);
            if (match is null) return null;
            _context.Entry(match).CurrentValues.SetValues(entity);
            return match;
        }
    }
}
