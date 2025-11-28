using ProjectHK3.Application.DTOs.Claim;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IClaimService
    {
        Task<int> SubmitClaimAsync(SubmitClaimRequest request);
        Task<ClaimDetailDto?> GetClaimByIdAsync(int claimId);
        Task<IEnumerable<ClaimListDto>> GetClaimByEmployeeAsync(int employeeId);
        Task<IEnumerable<ClaimListDto>> GetPendingClaimAsync();
        Task<bool> ApproveClaimAsync(int claimId, int adminId, string remarks);
        Task<bool> RejectClaimAsync(int claimId, int adminId, string remarks);
        Task<bool> UploadClaimDocumentAsync(int claimId, IEnumerable<ClaimDocumentDto> documents);
        Task<IEnumerable<ClaimDocumentDto>> GetClaimDocumentsAsync(int claimId);
        Task<bool> UpdateClaimStatusAsync(int claimId, string newStatus);
        Task<decimal> CalculateReimbursableAmountAsync(int claimId);
        Task<bool> DisburseClaimAmountAsync(int claimId, decimal amount);
        Task<IEnumerable<ClaimHistoryDto>> GetClaimHistoryAsync(int claimId);
    }
}
