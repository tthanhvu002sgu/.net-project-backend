using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> GetPatientIdByEmail(string email)
        {
            var patientId = await _context.Patients.Where(p => p.email == email).Select(p => p.patientId).FirstOrDefaultAsync();
            return patientId;
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


        public List<ApplicationUser> GetAll()
        {
            var patients = _userManager.Users.Where(u => !u.Email.EndsWith("@doctor.hospital.com")).ToList();
            //var patients = await _userManager.Users.Where(u => !u.Email.EndsWith("@doctor.hospital.com")).Select(u => u).ToListAsync();
            return patients;
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
