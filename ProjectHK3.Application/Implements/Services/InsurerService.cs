using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Insurer;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Implements.Services
{
    public class InsurerService(
        ICompanyDetailRepo companyDetailRepo,
        IUnitOfWork unitOfWork,
        ILogger<InsurerService> logger
        ) : IInsurerService
    {
        readonly ICompanyDetailRepo _companyDetailRepo = companyDetailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly ILogger<InsurerService> _logger = logger;
        public async Task<int> CreateInsurerAsync(CreateInsurerRequest request)
        {
            try
            {
                CompanyDetail company = new()
                {
                    CompanyName = request.CompanyName,
                    Address = request.Address,
                    ContactEmail = new EmailAddress(request.ContactEmail),
                    ContactPhone = new PhoneNumber(request.ContactPhone),
                    Website = request.Website,
                    CreateBy = request.CreateBy,
                };
                var result = await _companyDetailRepo.AddOneAsync(company) ?? throw new BusinessException("Create new Insurer failed", 400);
                await _unitOfWork.SaveChangesAsync();
                return result.Id;
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

        public async Task<bool> DeleteInsurerAsync(int insurerId)
        {
            try
            {
                var result = await _companyDetailRepo.DeleteOneAsync(insurerId);
                await _unitOfWork.SaveChangesAsync();
                return result;
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

        public async Task<IEnumerable<InsurerListDto>> GetAllInsurersAsync(bool includeDeleted)
        {
            try
            {
                var insurers = await _companyDetailRepo.GetAllAsync();
                return insurers
                    .Where(i => includeDeleted || i.IsDeleted == false)
                    .Select(i => new InsurerListDto()
                    {
                        Id = i.Id,
                        CompanyName = i.CompanyName ?? string.Empty,
                        Address = i.Address ?? string.Empty,
                        ContactEmail = i.ContactEmail!.ToString(),
                        Website = i.Website ?? string.Empty
                    }
                    );
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

        public async Task<InsurerDetailDto?> GetInsurerByIdAsync(int insurerId)
        {
            try
            {
                var match = await _companyDetailRepo.GetOneAsync(insurerId) ?? throw new BusinessException("Insurer not found", 404);
                return new InsurerDetailDto()
                {
                    Id = match.Id,
                    CreatedAt = match.CreatedAt,
                    UpdatedAt = match.UpdatedAt,
                    CreatedBy = match.CreatedBy ?? string.Empty,
                    UpdatedBy = match.UpdatedBy ?? string.Empty,
                    CompanyName = match.CompanyName ?? string.Empty,
                    Address = match.Address ?? string.Empty,
                    ContactEmail = match.ContactEmail?.ToString() ?? string.Empty,
                    ContactPhone = match.ContactPhone?.ToString() ?? string.Empty,
                    Website = match.Website ?? string.Empty,
                    CreateBy = match.CreateBy,
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

        public async Task<bool> RestoreAsync(int insurerId)
        {
            try
            {
                var result = await _companyDetailRepo.ReStoreById(insurerId);
                await _unitOfWork.SaveChangesAsync();
                return result;
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

        public async Task<IEnumerable<InsurerListDto>> SearchInsurerAsync(string query)
        {
            try
            {
                var all = await _companyDetailRepo.GetAllAsync();
                return all
                    .Where(a => a.CompanyName!.ToLower().Contains(query.ToLower()) && a.IsDeleted == false)
                    .Select(i => new InsurerListDto()
                    {
                        Id = i.Id,
                        CompanyName = i.CompanyName ?? string.Empty,
                        Address = i.Address ?? string.Empty,
                        ContactEmail = i.ContactEmail!.ToString(),
                        Website = i.Website ?? string.Empty
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

        public async Task<bool> SoftDeleteAsync(int insurerId)
        {
            try
            {
                var result = await _companyDetailRepo.DeleteOneAsync(insurerId);
                await _unitOfWork.SaveChangesAsync();
                return result;
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

        public async Task<bool> UpdateInsurerAsync(int insurerId, UpdateInsurerRequest request)
        {
            try
            {
                var match = await _companyDetailRepo.GetOneAsync(insurerId) ?? throw new BusinessException("Insurer not found", 404);
                match.CompanyName = request.CompanyName ?? match.CompanyName;
                match.Address = request.Address ?? match.Address;
                match.ContactEmail = request.ContactEmail != null ? new EmailAddress(request.ContactEmail) : match.ContactEmail;
                match.ContactPhone = request.ContactPhone != null ? new PhoneNumber(request.ContactPhone) : match.ContactPhone;
                match.Website = request.Website ?? match.Website;
                var result = await _companyDetailRepo.UpdateOneAsync(match);
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
