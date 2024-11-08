using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;

namespace DoAn_API.Services
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PatientRepository(MyDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public void Delete(int id)
        {
            var patient = _context.Patients.SingleOrDefault(patient => patient.patientId == id);
            if (patient != null)
            {


                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        //public List<PatientVM> GetAll()
        //{

        //    var patients = _context.Patients.Select(patient => new PatientVM { userId = patient.patientId, email = patient.email, password = patient.password, image = patient.image, phoneNumber = patient.phoneNumber, fullName = patient.fullName, address = patient.address, gender = patient.gender, dob = patient.dob });
        //    return patients.ToList();
        //}

        public async Task<ApplicationUser> GetPatientByEmailAsync(string email)
        {
            var patient = await _userManager.FindByEmailAsync(email);
            return patient;
        }
        public async Task<bool> UpdatePatient(string email, ApplicationUser patient)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.FullName = patient.FullName;
                user.Address = patient.Address;
                user.PhoneNumber = patient.PhoneNumber;
                user.Gender = patient.Gender;
                user.Dob = patient.Dob;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            return false;
        }
        //public void Update(PatientVM patient)
        //{
        //    var type = _context.Patients.SingleOrDefault(patient => patient.userId == patient.userId);

        //    if (type != null)
        //    {


        //        type.email = patient.email;
        //        type.password = patient.password;
        //        type.image = patient.image;
        //        type.phoneNumber = patient.phoneNumber;
        //        type.fullName = patient.fullName;
        //        type.address = patient.address;
        //        type.gender = patient.gender;
        //        type.dob = patient.dob;
        //        _context.SaveChanges();
        //    }
        //}


        public List<PatientVM> GetAll()
        {
            throw new NotImplementedException();
        }



        public void Update(PatientVM patient)
        {
            throw new NotImplementedException();
        }

        public PatientVM GetPatientVM(int id)
        {
            throw new NotImplementedException();
        }
    }
}
