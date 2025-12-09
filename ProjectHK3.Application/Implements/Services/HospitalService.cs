using Microsoft.Extensions.Logging;
using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Hospital;
using ProjectHK3.Application.Exceptions;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Implements.Services
{
    public class HospitalService(
        IHospitalInfoRepo hospitalInfoRepo,
        IUnitOfWork unitOfWork,
        ILogger<HospitalService> logger
        ) : IHospitalService
    {
        readonly IHospitalInfoRepo _hospitalInfoRepo = hospitalInfoRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        readonly ILogger<HospitalService> _logger = logger;
        public async Task<int?> CreateNewAsync(CreateHospital request)
        {
            try
            {
                var exists = (await _hospitalInfoRepo.GetAllAsync()).Any(h => h.HospitalName == request.HospitalName && !h.IsDeleted);
                if (exists) throw new BusinessException("Hospital already exists.", 400);

                HospitalInfo hospital = new HospitalInfo()
                {
                    HospitalName = request.HospitalName,
                    Address = request.Address,
                    City = request.City,
                    ContactPhone = new PhoneNumber(request.ContactPhone),
                    Email = new EmailAddress(request.Email),
                    RegisteredOn = request.RegisteredOn,
                };
                var result = await _hospitalInfoRepo.AddOneAsync(hospital);
                await _unitOfWork.SaveChangesAsync();
                if (result is null) return null;
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

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var found = await _hospitalInfoRepo.GetOneAsync(id) ?? throw new BusinessException("Hospital not found.", 404);

                var result = await _hospitalInfoRepo.DeleteOneAsync(id);
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

        public async Task<IEnumerable<ListHospital>> GetAllAsync(bool includeDeleted = false)
        {
            try
            {
                var hospitals = await _hospitalInfoRepo.GetAllAsync();
                return hospitals
                    .Where(h => includeDeleted || h.IsDeleted == false)
                    .OrderByDescending(h => h.CreatedAt)
                    .Select(h => new ListHospital()
                    {
                        Id = h.Id,
                        HospitalName = h.HospitalName,
                        Address = h.Address,
                        City = h.City,
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

        public async Task<DetailHospital?> GetOneByIdAsync(int id)
        {
            try
            {
                var match = await _hospitalInfoRepo.GetOneAsync(id);
                if (match is null) return null;
                return new DetailHospital
                {
                    Id = match.Id,
                    CreatedAt = match.CreatedAt,
                    UpdatedAt = match.UpdatedAt,
                    CreatedBy = match.CreatedBy ?? "",
                    UpdatedBy = match.UpdatedBy ?? "",

                    HospitalName = match.HospitalName,
                    Address = match.Address,
                    City = match.City,
                    ContactPhone = match.ContactPhone?.ToString(),
                    Email = match.Email?.ToString(),
                    RegisteredOn = match.RegisteredOn
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

        public async Task<bool> UpdateByIdAsync(int id, UpdateHospital request)
        {
            try
            {
                var match = await _hospitalInfoRepo.GetOneAsync(id);
                if (match is null) return false;
                match.HospitalName = request.HospitalName ?? match.HospitalName;
                match.Address = request.Address ?? match.Address;
                match.City = request.City ?? match.City;

                if (request.ContactPhone != null)
                    match.ContactPhone = new PhoneNumber(request.ContactPhone);

                if (request.Email != null)
                    match.Email = new EmailAddress(request.Email);

                await _hospitalInfoRepo.UpdateOneAsync(match);
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
