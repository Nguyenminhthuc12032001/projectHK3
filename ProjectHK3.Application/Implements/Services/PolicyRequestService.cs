using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IRepositories.Authentication;
using ProjectHK3.Application.Abstractions.IRepositories.Policy_Enrollment___Claims;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.PolicyRequest;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;

namespace ProjectHK3.Application.Implements.Services
{
    public class PolicyRequestService(
        IUnitOfWork unitOfWork,
        IPolicyRequestDetailRepo policyRequestDetailRepo,
        IPolicyRequestDocumentRepo policyRequestDocumentRepo,
        IPolicyRepo policyRepo,
        IEmpRegisterRepo empRegisterRepo,
        ILogger<PolicyRequestService> logger
        ) : IPolicyRequestService
    {
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly IPolicyRequestDetailRepo _policyRequestDetailRepo = policyRequestDetailRepo;
        readonly IPolicyRequestDocumentRepo _policyRequestDocumentRepo = policyRequestDocumentRepo;
        readonly IPolicyRepo _policyRepo = policyRepo;
        readonly IEmpRegisterRepo _empRegisterRepo = empRegisterRepo;
        readonly ILogger<PolicyRequestService> _logger = logger;
        public async Task<DetailPolicyRequestDto?> GetByIdAsync(int requestId)
        {
            try
            {
                var match = await _policyRequestDetailRepo.GetOneAsync(requestId);
                if (match == null) return null;
                var documents = await _policyRequestDocumentRepo.GetAllAsync();
                var matchDocuments = documents
                    .Where(d => d.RequestId == requestId)
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

        public async Task<IEnumerable<PolicyRequestDocumentDto>> GetDocumentsAsync(int requestId)
        {
            try
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

        public async Task<IEnumerable<ListPolicyRequestDto>> GetPendingRequestsAsync()
        {
            try
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

        public async Task<IEnumerable<ListPolicyRequestDto>> GetRequestByEmployeeId(int employeeId)
        {
            try
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

        public async Task<int?> SubmitRequestAsync(CreatePolicyRequestDto request)
        {
            try
            {
                var policy = await _policyRepo.GetOneAsync(request.PolicyId);
                if (policy == null)
                    throw new BusinessException("Policy does not exist.", 404);

                var employee = await _empRegisterRepo.GetOneAsync(request.EmployeeId);
                if (employee == null)
                    throw new BusinessException("Employee does not exist.", 404);

                var all = await _policyRequestDetailRepo.GetAllAsync();
                var exist = all.Any(a =>
                    a.EmployeeId == request.EmployeeId &&
                    a.IsDeleted == false &&
                    a.Status == StatusOfPolicyRequestDetail.Pending
                );

                if (exist)
                    throw new BusinessException("Employee already has a pending request.", 409);

                var entity = new PolicyRequestDetail
                {
                    PolicyId = request.PolicyId,
                    EmployeeId = request.EmployeeId,
                    RequestType = Enum.Parse<RequestTypeOfPolicyRequestDetail>(request.RequestType),
                    Remarks = request.Remarks,
                    RequestDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Status = StatusOfPolicyRequestDetail.Pending
                };

                await _policyRequestDetailRepo.AddOneAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return entity.Id;
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

        public async Task<bool> UpdateDocumentsAsync(int requestId, IEnumerable<PolicyRequestDocumentDto> documents)
        {
            try
            {
                var all = await _policyRequestDocumentRepo.GetAllAsync();
                var matchs = all.Where(a => a.RequestId == requestId && a.IsDeleted == false);
                if (!matchs.Any()) return false;
                foreach (var match in matchs)
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
                            UploadedBy = document.UploadedBy,
                            Status = StatusOfPolicyRequestDocument.Pending
                        });
                    }
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

        public async Task<bool> UpdateRequestAsync(int requestId, UpdatePolicyRequestDto request)
        {
            try
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
                await _unitOfWork.SaveChangesAsync();
                return result != null;
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
