namespace ProjectHK3.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
