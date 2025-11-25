using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Services
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        readonly ApplicationDbContext _context = context;
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
