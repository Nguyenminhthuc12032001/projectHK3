using ProjectHK3.Application.DTOs.Finance;

namespace ProjectHK3.Application.Abstractions.IServices
{
    public interface IFinanceService
    {
        Task<bool> CreateDisbursementRequestAsync(int claimId);
        Task<bool> ApproveDisbursementAsync(int transactionLedgerId, int adminId);
        Task<bool> RejectDisbursementAsync(int transactionLedgerId, int adminId, string? remarks);
        Task<bool> ExecutePaymentAsync(int transactionLedgerId);
        Task<DisbursementDetailDto?> GetDisbursementByIdAsync(int disbursementId);
        Task<IEnumerable<DisbursementListDto>> GetPendingDisbursementsAsync();
        Task<decimal> CalculateFinalAmountAsync(int claimId);
        Task<IEnumerable<TransactionHistoryDto>> GetTransactionHistoryAsync(int claimId);

    }
}
