using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoAn_API.Services
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MyDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DoctorRepository(MyDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Delete(int id)
        {
        }
        public async Task<int> GetDoctorIdByEmail(string email)
        {
            var doctorId = await _context.Doctors
                .Where(d => d.email == email)
                .Select(d => d.doctorId)
                .FirstOrDefaultAsync();

            return doctorId;
        }
        public async Task<ApplicationUser> GetDoctorInfoByEmail(string email)
        {
            var doctor = await _userManager.FindByEmailAsync(email);
            return doctor;

        }
        public async Task<List<DoctorVM>> GetAllDoctorsAsync()
        {
            try
            {

                var doctors = await _context.Doctors
                    .Select(d => new DoctorVM
                    {
                        specializationName = d.specializationName ?? string.Empty,
                        doctorName = d.doctorName ?? string.Empty,
                        doctorImg = d.doctorImage ?? string.Empty,
                        email = d.email ?? string.Empty,
                        degree = d.degree ?? string.Empty,
                        experience = d.experience ?? 0.0,
                        bookingFee = d.bookingFee ?? 0.0,
                        doctorAbout = d.doctorAbout ?? string.Empty,
                        isAvailable = d.isAvailable
                    })
                    .ToListAsync();

                return doctors;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving doctors from database", ex);
            }
        }


        public async Task<bool> UpdateAvailabilityAsync(string doctorEmail, bool isAvailable)
        {
            var doctor = _context.Doctors.SingleOrDefault(d => d.email == doctorEmail);
            if (doctor == null)
            {
                return false;
            }

            doctor.isAvailable = isAvailable;
            _context.Doctors.Update(doctor);
            _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<DoctorVM>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var doctors = await _context.Doctors
                .Where(d => d.specializationName == specialization)
                .Select(d => new DoctorVM
                {
                    specializationName = d.specializationName ?? string.Empty,
                    doctorName = d.doctorName ?? string.Empty,
                    doctorImg = d.doctorImage ?? string.Empty,
                    email = d.email ?? string.Empty,
                    degree = d.degree ?? string.Empty,
                    experience = d.experience ?? 0.0,
                    bookingFee = d.bookingFee ?? 0.0,
                    doctorAbout = d.doctorAbout ?? string.Empty,
                    isAvailable = d.isAvailable
                })
                .ToListAsync();
            return doctors;
        }


        public DoctorVM GetDoctorVM(int doctorId)
        {
            throw new NotImplementedException();
        }

        public void Update(int doctorId, DoctorVM doctor)
        {
        }


        public async Task<DoctorVM> GetDoctorByEmail(string doctorEmail)
        {
            var doctor = await _context.Doctors
                .Where(d => d.email == doctorEmail)
                .Select(d => new DoctorVM
                {
                    specializationName = d.specializationName ?? string.Empty,
                    doctorName = d.doctorName ?? string.Empty,
                    doctorImg = d.doctorImage ?? string.Empty,
                    email = d.email ?? string.Empty,
                    degree = d.degree ?? string.Empty,
                    experience = d.experience ?? 0.0,
                    bookingFee = d.bookingFee ?? 0.0,
                    doctorAbout = d.doctorAbout ?? string.Empty,
                    isAvailable = d.isAvailable
                })
                .FirstOrDefaultAsync();
            return doctor;
        }




    }
}
