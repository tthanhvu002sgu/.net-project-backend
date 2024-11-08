using DoAn_API.Data;
using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IPatientRepository
    {
        public List<PatientVM> GetAll();
        Task<ApplicationUser> GetPatientByEmailAsync(string email);
        Task<bool> UpdatePatient(string email, ApplicationUser patient);
        void Delete(int id);

    }
}
