using ProjectHK3.Domain.Emtitys;

namespace ProjectHK3.Domain.Abstractions
{
    internal interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
