namespace ProjectHK3.Domain.Abstractions
{
    internal interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
