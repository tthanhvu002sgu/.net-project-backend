using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;

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

        public List<DoctorVM> GetAllAsync()
        {

            var doctors = _context.Doctors.Select(d => new DoctorVM { specializationName = d.specializationName, email = d.email, degree = d.degree, experience = (double)d.experience, bookingFee = (double)d.bookingFee, doctorAbout = d.doctorAbout });
            return doctors.ToList();
        }

        public DoctorVM GetDoctorVM(int doctorId)
        {
            throw new NotImplementedException();
        }

        public void Update(int doctorId, DoctorVM doctor)
        {
        }



        PatientVM IDoctorRepository.GetDoctorVM(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
