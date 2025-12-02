using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyRequest;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyRequestService(
        IUnitOfWork unitOfWork,
        IPolicyRequestDetailRepo policyRequestDetailRepo,
        IPolicyRequestDocumentRepo policyRequestDocumentRepo,
        IPolicyRepo policyRepo,
        IEmpRegisterRepo empRegisterRepo
        ) : IPolicyRequestService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IPolicyRequestDetailRepo _policyRequestDetailRepo = policyRequestDetailRepo;
        readonly IPolicyRequestDocumentRepo _policyRequestDocumentRepo = policyRequestDocumentRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IEmpRegisterRepo _empRegisterRepo = empRegisterRepo;
        public async Task<DetailPolicyRequestDto?> GetByIdAsync(int requestId)
        {
            var match = await _policyRequestDetailRepo.GetOneAsync(requestId);
            if (match == null) return null;
            var documents = await _policyRequestDocumentRepo.GetAllAsync();
            var matchDocuments = documents
                .Where(d => d.RequestId ==  requestId)
                .Select(d => new PolicyRequestDocumentDto
                {
                    Id = d.Id,
                    FileName = d.FileName ?? string.Empty,
                    FileURL = d.FileURL ?? string.Empty,
                    DocumentType = d.DocumentType.ToString(),
                    Status = d.Status.ToString(),
                    UploadedBy = d.UploadedBy,
                });
            return new DetailPolicyRequestDto
            {
                Id = match.Id,
                CreatedAt = match.CreatedAt,
                UpdatedAt = match.UpdatedAt,
                CreatedBy = match.CreatedBy ?? "",
                UpdatedBy = match.UpdatedBy ?? "",

                PolicyName = match.Policy?.PolicyName ?? string.Empty,
                EmployeeName = match.Employee?.FullName ?? string.Empty,
                RequestType = match.RequestType.ToString(),
                Status = match.Status.ToString(),
                RequestDate = match.RequestDate,
                Remarks = match.Remarks,
                Documents = matchDocuments
            };
        }

        public async Task<IEnumerable<PolicyRequestDocumentDto>> GetDocumentsAsync(int requestId)
        {
            var documents = await _policyRequestDocumentRepo.GetAllAsync();
            return documents
                .Where(d => d.RequestId == requestId && d.IsDeleted == false)
                .Select(d => new PolicyRequestDocumentDto
                {
                    Id = d.Id,
                    FileName = d.FileName ?? string.Empty,
                    FileURL = d.FileURL ?? string.Empty,
                    DocumentType = d.DocumentType.ToString(),
                    Status = d.Status.ToString(),
                    UploadedBy = d.UploadedBy,
                });
        }

        public async Task<IEnumerable<ListPolicyRequestDto>> GetPendingRequestsAsync()
        {
            var all = await _policyRequestDetailRepo.GetAllAsync();
            return all
                .Where(a => a.Status == StatusOfPolicyRequestDetail.Pending && a.IsDeleted == false)
                .Select(d => new ListPolicyRequestDto
                {
                    Id = d.Id,
                    PolicyName = d.Policy?.PolicyName ?? string.Empty,
                    RequestType = d.RequestType.ToString(),
                    Status = d.Status.ToString(),
                    RequestDate = d.RequestDate
                });
        }

        public async Task<IEnumerable<ListPolicyRequestDto>> GetRequestByEmployeeId(int employeeId)
        {
            var all = await _policyRequestDetailRepo.GetAllAsync();
            return all
                .Where(a => a.EmployeeId == employeeId && a.IsDeleted == false)
                .Select(a => new ListPolicyRequestDto
                {
                    Id = a.Id,
                    PolicyName = a.Policy?.PolicyName ?? string.Empty,
                    RequestType = a.RequestType.ToString(),
                    Status = a.Status.ToString(),
                    RequestDate = a.RequestDate
                });
        }

        public async Task<int?> SubmitRequestAsync(CreatePolicyRequestDto request)
        {
            var all = await _policyRequestDetailRepo.GetAllAsync();
            var exist = all.Any(a => a.EmployeeId == request.EmployeeId && a.Status == StatusOfPolicyRequestDetail.Pending && a.IsDeleted == false);
            if (exist) return null;
            var result = await _policyRequestDetailRepo.AddOneAsync(new PolicyRequestDetail
            {
                PolicyId = request.PolicyId,
                EmployeeId = request.EmployeeId,
                RequestType = request.RequestType switch
                {
                    "Enrollment" => RequestTypeOfPolicyRequestDetail.Enrollment,
                    "Claim" => RequestTypeOfPolicyRequestDetail.Claim,
                    _ => RequestTypeOfPolicyRequestDetail.Enrollment
                },
                Remarks = request.Remarks
            });
            await _unitOfWork.SaveChangesAsync();
            return result?.Id;
        }

        public async Task<bool> UpdateDocumentsAsync(int requestId, IEnumerable<PolicyRequestDocumentDto> documents)
        {
            var all = await _policyRequestDocumentRepo.GetAllAsync();
            var matchs = all.Where(a => a.RequestId == requestId && a.IsDeleted == false);
            if (!matchs.Any()) return false;
            foreach(var match in matchs)
            {
                if (!documents.Any(d => d.FileURL == match.FileURL))
                {
                    await _policyRequestDocumentRepo.DeleteOneAsync(match.Id);
                }
            }

            foreach (var document in documents)
            {
                if (!matchs.Any(m => m.FileURL == document.FileURL))
                {
                    await _policyRequestDocumentRepo.AddOneAsync(new PolicyRequestDocument
                    {
                        RequestId = requestId,
                        FileName = document.FileName,
                        FileURL = document.FileURL,
                        DocumentType = document.DocumentType switch
                        {
                            "Invoice" => DocumentTypeOfPolicyRequestDocument.Invoice,
                            "Medical" => DocumentTypeOfPolicyRequestDocument.Medical,
                            "IDCard" => DocumentTypeOfPolicyRequestDocument.IDCard,
                            "Other" => DocumentTypeOfPolicyRequestDocument.Other,
                            _ => DocumentTypeOfPolicyRequestDocument.Invoice
                        },
                        UploadedBy = document.UploadedBy
                    });
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRequestAsync(int requestId, UpdatePolicyRequestDto request)
        {
            var match = await _policyRequestDetailRepo.GetOneAsync(requestId);
            if (match == null) return false;
            match.Status = request.Status switch
            {
                "Pending" => StatusOfPolicyRequestDetail.Pending,
                "Approved" => StatusOfPolicyRequestDetail.Approved,
                "Rejected" => StatusOfPolicyRequestDetail.Rejected,
                "NeedInfo" => StatusOfPolicyRequestDetail.NeedInfo,
                _ => StatusOfPolicyRequestDetail.Pending
            };
            match.Remarks = request.Remarks;
            var result = await _policyRequestDetailRepo.UpdateOneAsync(match);
            return result != null;
        }
    }
}
