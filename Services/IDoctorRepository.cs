﻿using DoAn_API.Data;
using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IDoctorRepository
    {
        Task<List<DoctorVM>> GetAllDoctorsAsync();
        Task<DoctorVM> GetDoctorByEmail(string doctorEmail);
        Task<List<DoctorVM>> GetDoctorsBySpecializationAsync(string specialization);
        Task<bool> UpdateDoctorAsync(string email, DoctorVM doctorVM);
        Task<bool> UpdateAvailabilityAsync(string doctorEmail, bool isAvailable);
        void Delete(int id);
        Task<int> GetDoctorIdByEmail(string doctorEmail);
        Task<ApplicationUser> GetDoctorInfoByEmail(string doctorEmail);

    }
}
