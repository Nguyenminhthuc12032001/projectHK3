using ProjectHK3.Application.DTOs.PolicyRequest;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IPolicyRequestService
    {
        Task<int> SubmitRequestAsync(CreatePolicyRequestDto request);
        Task<bool> UpdateRequestAsync(int requestId, UpdatePolicyRequestDto request);
        Task<DetailPolicyRequestDto?> GetByIdAsync(int requestId);
        Task<IEnumerable<ListPolicyRequestDto>> GetRequestByEmployeeId(int employeeId);
        Task<bool> UpdateDocumentsAsync(int requestId, IEnumerable<PolicyRequestDocumentDto> documents);
        Task<IEnumerable<PolicyRequestDocumentDto>> GetDocumentsAsync(int requestId);
        Task<IEnumerable<ListPolicyRequestDto>> GetPendingRequestsAsync();

    }
}
