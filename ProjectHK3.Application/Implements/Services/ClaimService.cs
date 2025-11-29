using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Claim;
using ProjectHK3.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Application.Implements.Services
{
    internal class ClaimService(IUnitOfWork unitOfWork) : IClaimService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        public Task<bool> ApproveClaimAsync(int claimId, int adminId, string remarks)
        {

        }

        public Task<decimal> CalculateReimbursableAmountAsync(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisburseClaimAmountAsync(int claimId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClaimListDto>> GetClaimByEmployeeAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimDetailDto?> GetClaimByIdAsync(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClaimDocumentDto>> GetClaimDocumentsAsync(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClaimHistoryDto>> GetClaimHistoryAsync(int claimId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClaimListDto>> GetPendingClaimAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RejectClaimAsync(int claimId, int adminId, string remarks)
        {
            throw new NotImplementedException();
        }

        public Task<int> SubmitClaimAsync(SubmitClaimRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateClaimStatusAsync(int claimId, string newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadClaimDocumentAsync(int claimId, IEnumerable<ClaimDocumentDto> documents)
        {
            throw new NotImplementedException();
        }
    }
}
