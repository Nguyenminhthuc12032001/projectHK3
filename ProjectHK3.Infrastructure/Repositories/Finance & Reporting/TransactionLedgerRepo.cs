using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Finance___Reporting
{
    public class TransactionLedgerRepo(ApplicationDbContext context) : ITransactionLedgerRepo
    {
        readonly ApplicationDbContext _context = context;

        public async Task<TransactionLedger?> AddOneAsync(TransactionLedger entity)
        {
            await _context.TransactionLedger.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var match = await _context.TransactionLedger.FindAsync(id);
            if (match is null) return false;
            _context.TransactionLedger.Remove(match);
            return true;
        }

        public async Task<IEnumerable<TransactionLedger>> GetAllAsync()
        {
            return await _context.TransactionLedger.ToListAsync();
        }

        public async Task<TransactionLedger?> GetOneAsync(int id)
        {
            return await _context.TransactionLedger.FindAsync(id);
        }

        public async Task<TransactionLedger?> UpdateOneAsync(TransactionLedger entity)
        {
            var match = await _context.TransactionLedger.FindAsync(entity.Id);
            if (match is null) return null;
            _context.TransactionLedger.Update(entity);
            return match;
        }
    }
}
