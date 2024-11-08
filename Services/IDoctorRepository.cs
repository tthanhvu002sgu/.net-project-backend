using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IDoctorRepository
    {
        List<DoctorVM> GetAllDoctorsAsync();
        DoctorVM GetDoctorVM(int doctorId);
        void Update(int doctorId, DoctorVM doctor);
        void Delete(int id);
    }
}
