namespace ProjectHK3.Application.Abstractions.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> AddOneAsync(T entity);
        Task<T?> GetOneAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> UpdateOneAsync(T entity);
        Task<bool> DeleteOneAsync(int id);
        Task<bool> ReStoreById(int id);
    }
}
