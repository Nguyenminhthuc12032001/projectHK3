using ProjectHK3.Application.DTOs.Hospital;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IHospitalService
    {
        Task<int?> CreateNewAsync(CreateHospital request);
        Task<DetailHospital?> GetOneByIdAsync(int id);
        Task<IEnumerable<ListHospital>> GetAllAsync(bool includeDeleted = false);
        Task<bool> UpdateByIdAsync(int id, UpdateHospital request);
        Task<bool> DeleteByIdAsync(int id);  
    }
}
