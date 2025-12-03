using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Infrastructure.Persistence;

namespace ProjectHK3.Infrastructure.Repositories.Authentication
{
    public class EmpRegisterRepo(ApplicationDbContext context) : IEmpRegisterRepo
    {
        readonly ApplicationDbContext _context = context;
        public Task<EmpRegister?> AddOneAsync(EmpRegister entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmpRegister>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmpRegister?> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmpRegister?> UpdateOneAsync(EmpRegister entity)
        {
            throw new NotImplementedException();
        }
    }
}
