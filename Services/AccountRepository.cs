using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoAn_API.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, MyDbContext context)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> SignInAsync(SignInVM model)
        {
            var user = await userManager.FindByEmailAsync(model.email);
            var passwordValid = await userManager.CheckPasswordAsync(user, model.password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }
            var result = await signInManager.PasswordSignInAsync(model.email, model.password, false, false);
            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }



        public async Task<IdentityResult> SignUpAsync(SignUpVM model)
        {
            var user = new ApplicationUser
            {
                UserName = model.email,
                Email = model.email,
                FullName = model.fullName,
                Image = model.image,
                Gender = model.gender,
                Dob = model.dob,
                Address = model.address,
            };
            var result = await userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                string role = DetermineRoleFromEmail(model.email);

                if (role == AppRole.Patient)
                {
                    var patient = new Patient
                    {
                        patientId = _context.Patients.Count() + 1,
                        email = model.email
                        // Các thuộc tính khác của bệnh nhân
                    };
                    _context.Patients.Add(patient);
                }
                else if (role == AppRole.Doctor)
                {
                    var doctor = new Doctor
                    {
                        doctorId = _context.Doctors.Count() + 1,
                        email = model.email
                        // Các thuộc tính khác của bác sĩ
                    };
                    _context.Doctors.Add(doctor);
                }

                await _context.SaveChangesAsync();

                // Tạo role nếu chưa tồn tại
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRoleAsync(user, AppRole.Patient);
            }
            return result;
        }
        private string DetermineRoleFromEmail(string email)
        {
            // Ví dụ phân biệt bằng domain
            if (email.EndsWith("@admin.hospital.com"))
                return AppRole.Admin;
            else if (email.EndsWith("@doctor.hospital.com"))
                return AppRole.Doctor;
            else
                return AppRole.Patient;
        }

    }
}
