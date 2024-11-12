using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IDoctorRepository
    {
        Task<List<DoctorVM>> GetAllDoctorsAsync();
        DoctorVM GetDoctorVM(int doctorId);
        Task<List<DoctorVM>> GetDoctorsBySpecializationAsync(string specialization);
        void Update(int doctorId, DoctorVM doctor);
        Task<bool> UpdateAvailabilityAsync(string doctorEmail, bool isAvailable);
        void Delete(int id);
    }
}
