using DoAn_API.Models;

namespace DoAn_API.Services
{
    public interface IPatientRepository
    {
        public List<PatientVM> GetAll();
        PatientVM GetPatientVM(int id);
        PatientVM Add(PatientVM patient);
        void Update(PatientVM patient);
        void Delete(int id);

    }
}
