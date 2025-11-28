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
        public Task<PoliciesOnEmployee?> AddOneAsync(PoliciesOnEmployee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PoliciesOnEmployee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PoliciesOnEmployee?> GetOneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PoliciesOnEmployee?> UpdateOneAsync(PoliciesOnEmployee entity)
        {
            throw new NotImplementedException();
        }
    }
}
