using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IDoctorRepository
    {
        public List<DoctorVM> GetAllAsync();
        PatientVM GetDoctorVM(int doctorId);
        void Update(int doctorId, DoctorVM doctor);
        void Delete(int id);
    }
}
