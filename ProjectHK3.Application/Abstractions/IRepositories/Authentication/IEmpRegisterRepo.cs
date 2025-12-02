using ProjectHK3.Domain.Emtitys;

namespace ProjectHK3.Application.Abstractions.IRepositories.Authentication
{
    public interface IEmpRegisterRepo : IBaseRepository<EmpRegister>
    {
        Task<bool> ReStoreById(int id);
    }
}
