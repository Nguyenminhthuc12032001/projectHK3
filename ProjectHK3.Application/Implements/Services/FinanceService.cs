using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;

using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Finance;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;


namespace ProjectHK3.Application.Implements.Services
{
    public class FinanceService(
        ITransactionLedgerRepo transactionLedgerRepo,
        IPolicyApprovalDetailRepo policyApprovalDetailRepo,
        IUnitOfWork unitOfWork,
        ILogger<FinanceService> logger
        ) : IFinanceService
    {
        readonly ITransactionLedgerRepo _transactionLedgerRepo = transactionLedgerRepo;
        readonly IPolicyApprovalDetailRepo _policyApprovalDetailRepo = policyApprovalDetailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly ILogger<FinanceService> _logger = logger;

        public async Task<bool> ApproveDisbursementAsync(int transactionLedgerId, int adminId)
        {
            try
            {
                var transaction = await _transactionLedgerRepo.GetOneAsync(transactionLedgerId);
                if (transaction == null || transaction.IsDeleted) return false;
                if (transaction.Status != StatusOfTransactionLedger.Pending) return false;

                transaction.Status = StatusOfTransactionLedger.Reserved;
                await _transactionLedgerRepo.UpdateOneAsync(transaction);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<decimal> CalculateFinalAmountAsync(int claimId)
        {
            try
            {
                var approval = await _policyApprovalDetailRepo.GetOneAsync(claimId);
                if (approval == null) return 0;

                var transactions = await _transactionLedgerRepo.GetAllAsync();
                var claimTransactions = transactions.Where(t =>
                    t.ApprovalId == claimId &&
                    !t.IsDeleted &&
                    t.Status == StatusOfTransactionLedger.Completed
                );

                return claimTransactions.Sum(t => t.Amount);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateDisbursementRequestAsync(int claimId)
        {
            try
            {
                var approval = await _policyApprovalDetailRepo.GetOneAsync(claimId);
                if (approval == null || approval.IsDeleted) return false;
                if (approval.Status != StatusOfPolicyApprovalDetail.Approved) return false;
                var transactions = await _transactionLedgerRepo.GetAllAsync();
                var existingTransaction = transactions.FirstOrDefault(t =>
                    t.ApprovalId == claimId &&
                    !t.IsDeleted
                );

                if (existingTransaction != null) return false;

                var newTransaction = new TransactionLedger
                {
                    ApprovalId = claimId,
                    Amount = 0,
                    Method = MethodOfTransactionLedger.BankTransfer,
                    Status = StatusOfTransactionLedger.Pending
                };

                await _transactionLedgerRepo.AddOneAsync(newTransaction);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> ExecutePaymentAsync(int transactionLedgerId)
        {
            try
            {
                var transaction = await _transactionLedgerRepo.GetOneAsync(transactionLedgerId);
                if (transaction == null || transaction.IsDeleted) return false;
                if (transaction.Status != StatusOfTransactionLedger.Reserved) return false;

                transaction.Status = StatusOfTransactionLedger.Completed;
                await _transactionLedgerRepo.UpdateOneAsync(transaction);

                // Update payment status in approval
                var approval = await _policyApprovalDetailRepo.GetOneAsync(transaction.ApprovalId);
                if (approval != null)
                {
                    approval.PaymentStatus = PaymentStatusOfPolicyApprovalDetail.Paid;
                    await _policyApprovalDetailRepo.UpdateOneAsync(approval);
                }

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<DisbursementDetailDto?> GetDisbursementByIdAsync(int disbursementId)
        {
            try
            {
                var transaction = await _transactionLedgerRepo.GetOneAsync(disbursementId);
                if (transaction == null || transaction.IsDeleted) return null;

                return new DisbursementDetailDto
                {
                    Id = transaction.Id,
                    ApprovalId = transaction.ApprovalId,
                    Amount = transaction.Amount,
                    Method = transaction.Method.ToString(),
                    Status = transaction.Status.ToString(),
                    CreatedAt = transaction.CreatedAt
                };
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DisbursementListDto>> GetPendingDisbursementsAsync()
        {
            try
            {
                var transactions = await _transactionLedgerRepo.GetAllAsync();

                var pending = transactions.Where(t =>
                    t.Status == StatusOfTransactionLedger.Pending &&
                    !t.IsDeleted
                );

                return pending.Select(t => new DisbursementListDto
                {
                    Id = t.Id,
                    ApprovalId = t.ApprovalId,
                    Amount = t.Amount,
                    Method = t.Method.ToString(),
                    Status = t.Status.ToString(),
                    CreatedAt = t.CreatedAt
                });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TransactionHistoryDto>> GetTransactionHistoryAsync(int claimId)
        {
            try
            {
                var transactions = await _transactionLedgerRepo.GetAllAsync();

                var claimTransactions = transactions.Where(t =>
                    t.ApprovalId == claimId &&
                    !t.IsDeleted
                );

                return claimTransactions.Select(t => new TransactionHistoryDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Method = t.Method.ToString(),
                    Status = t.Status.ToString(),
                    CreatedAt = t.CreatedAt
                });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> RejectDisbursementAsync(int transactionLedgerId, int adminId, string? remarks)
        {
            try
            {
                var transaction = await _transactionLedgerRepo.GetOneAsync(transactionLedgerId);
                if (transaction == null || transaction.IsDeleted) return false;
                if (transaction.Status != StatusOfTransactionLedger.Pending) return false;

                transaction.Status = StatusOfTransactionLedger.Failed;
                await _transactionLedgerRepo.UpdateOneAsync(transaction);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, "BUSINESS ERROR: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR: {Message}", ex.Message);
                throw;
            }
        }
    }
}
