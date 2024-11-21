using DoAn_API.Data;

namespace DoAn_API.Services
{
    public interface IPatientRepository
    {
        List<ApplicationUser> GetAll();
        Task<ApplicationUser> GetPatientByEmailAsync(string email);
        Task<bool> UpdatePatient(string email, ApplicationUser patient);
        void Delete(int id);
        Task<int> GetPatientIdByEmail(string patientEmail);
    }
}
