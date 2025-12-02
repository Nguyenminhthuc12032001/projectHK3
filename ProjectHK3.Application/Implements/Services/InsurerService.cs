using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Insurer;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Implements.Services
{
    public class InsurerService(
        ICompanyDetailRepo companyDetailRepo, 
        IUnitOfWork unitOfWork
        ) : IInsurerService
    {
        readonly ICompanyDetailRepo _companyDetailRepo = companyDetailRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<int> CreateInsurerAsync(CreateInsurerRequest request)
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
            await _companyDetailRepo.AddOneAsync( company );
            await _unitOfWork.SaveChangesAsync();
            return company.Id;
        }

        public async Task<bool> DeleteInsurerAsync(int insurerId)
        {
            await _companyDetailRepo.DeleteOneAsync(insurerId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InsurerListDto>> GetAllInsurersAsync(bool includeDeleted)
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

        public async Task<InsurerDetailDto?> GetInsurerByIdAsync(int insurerId)
        {
            var match = await _companyDetailRepo.GetOneAsync(insurerId);
            if (match is null) return null;
            return new InsurerDetailDto()
            {
                Id = match.Id,
                CreatedAt = match.CreatedAt,
                UpdatedAt = match.UpdatedAt,
                CreatedBy = match.CreatedBy ?? string.Empty,
                UpdatedBy = match.UpdatedBy ?? string.Empty,
                CompanyName =  match.CompanyName ?? string.Empty,
                Address = match.Address ?? string.Empty,
                ContactEmail = match.ContactEmail?.ToString() ?? string.Empty,
                ContactPhone = match.ContactPhone?.ToString() ?? string.Empty,
                Website = match.Website ?? string.Empty,
                CreateBy = match.CreateBy,
            };
        }

        public async Task<bool> RestoreAsync(int insurerId)
        {
            var result = await _companyDetailRepo.ReStoreById(insurerId);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public Task<IEnumerable<InsurerListDto>> SearchInsurerAsync(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteAsync(int insurerId)
        {
            return await _companyDetailRepo.DeleteOneAsync(insurerId);
        }

        public async Task<bool> UpdateInsurerAsync(int insurerId, UpdateInsurerRequest request)
        {
            var match = await _companyDetailRepo.GetOneAsync(insurerId);
            if (match is null) return false;
            match.CompanyName = request.CompanyName ?? match.CompanyName;
            match.Address = request.Address ?? match.Address;
            match.ContactEmail = request.ContactEmail != null ? new EmailAddress(request.ContactEmail) : match.ContactEmail;
            match.ContactPhone = request.ContactPhone != null ? new PhoneNumber(request.ContactPhone) : match.ContactPhone;
            match.Website = request.Website ?? match.Website;
            var result = await _companyDetailRepo.UpdateOneAsync(match);
            await _unitOfWork.SaveChangesAsync();
            return result != null;
        }
    }
}
