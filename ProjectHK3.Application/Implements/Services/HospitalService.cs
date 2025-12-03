using ProjectHK3.Application.Abstractions.IRepositories;
using ProjectHK3.Application.Abstractions.IServices;
using ProjectHK3.Application.DTOs.Hospital;
using ProjectHK3.Domain.Abstractions;
using ProjectHK3.Domain.Entities;
using ProjectHK3.Domain.ValueObjects;

namespace ProjectHK3.Application.Implements.Services
{
    public class HospitalService(
        IHospitalInfoRepo hospitalInfoRepo,
        IUnitOfWork unitOfWork
        ) : IHospitalService
    {
        readonly IHospitalInfoRepo _hospitalInfoRepo = hospitalInfoRepo;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<int?> CreateNewAsync(CreateHospital request)
        {
            HospitalInfo hospital = new HospitalInfo()
            {
                HospitalName = request.HospitalName,
                Address = request.Address,
                City = request.City,
                ContactPhone = new PhoneNumber(request.ContactPhone),
                Email = new EmailAddress(request.Email),
                RegisteredOn = request.RegisteredOn,
            };
            var result = await _hospitalInfoRepo.AddOneAsync( hospital );
            await _unitOfWork.SaveChangesAsync();
            if (result is null) return null;
            return result.Id;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var result = await _hospitalInfoRepo.DeleteOneAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<ListHospital>> GetAllAsync(bool includeDeleted = false)
        {
            var hospitals = await _hospitalInfoRepo.GetAllAsync();
            return hospitals
                .Where(h => includeDeleted || h.IsDeleted == false)
                .Select(h => new ListHospital()
                {
                    Id = h.Id,
                    HospitalName = h.HospitalName,
                    Address = h.Address,
                    City = h.City,
                });
        }

        public async Task<DetailHospital?> GetOneByIdAsync(int id)
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

        public async Task<bool> UpdateByIdAsync(int id, UpdateHospital request)
        {
            var match = await _hospitalInfoRepo.GetOneAsync(id);
            if (match is null) return false;
            var hospitalUpdate = new HospitalInfo
            {
                Id = match.Id,
                HospitalName = request.HospitalName,
                Address = request.Address,
                City = request.City,
            };
            if (request.ContactPhone != null) hospitalUpdate.ContactPhone = new PhoneNumber(request.ContactPhone);
            if (request.Email != null) hospitalUpdate.Email = new EmailAddress(request.Email);
            var result = await _hospitalInfoRepo.UpdateOneAsync(hospitalUpdate);
            await _unitOfWork.SaveChangesAsync();
            return result != null;
        }
    }
}
