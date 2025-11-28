using ProjectHK3.Application.DTOs.Audit;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IAuditService
    {
        Task LogAsync(AuditEntryDto entry);
        Task<IEnumerable<AuditTrailDto>> GetAuditLogAsync(AuditFilter filter);
        Task<AuditTrailDto> GetAuditByIdAsync(int id);
        Task<IEnumerable<AuditTrailDto>> GetByUserAsync(int userId);
        Task<IEnumerable<AuditTrailDto>> GetEntityAsync(string entityName, int entityId);
    }
}
