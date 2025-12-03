using ProjectHK3.Application.DTOs.Insurer;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IInsurerService
    {
        Task<int> CreateInsurerAsync(CreateInsurerRequest request);
        Task<bool> UpdateInsurerAsync(int insurerId, UpdateInsurerRequest request);
        Task<bool> DeleteInsurerAsync(int insurerId);
        Task<InsurerDetailDto?> GetInsurerByIdAsync(int insurerId);
        Task<IEnumerable<InsurerListDto>> GetAllInsurersAsync(bool includeDeleted);
        Task<IEnumerable<InsurerListDto>> SearchInsurerAsync(string query);
        Task<bool> SoftDeleteAsync(int insurerId);
        Task<bool> RestoreAsync(int insurerId);
    }
}
