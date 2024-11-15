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
            if (model.email == "admin@admin.hospital.com" && model.password == "admin@Abcd")
            {
                //var adminUser = await userManager.FindByEmailAsync(model.email);
                //if (adminUser == null)
                //{
                //    return string.Empty; // Return empty if the admin user is not found
                //}

                // Create claims for the admin
                var authClaimsAdmin = new List<Claim>
        {
            new Claim(ClaimTypes.Email, model.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, AppRole.Admin) // Assume "Admin" is the role name for admins
        };

                // Generate the JWT token
                var authenKeyAdmin = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                var adminToken = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaimsAdmin,
                    signingCredentials: new SigningCredentials(authenKeyAdmin, SecurityAlgorithms.HmacSha512Signature)
                );

                return new JwtSecurityTokenHandler().WriteToken(adminToken);
            }

            //for other user
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



        //public async Task<IdentityResult> SignUpAsync(SignupRequest request)
        //{
        //    var model = request.model;
        //    var doctorVM = request.doctorVM;
        //    var user = new ApplicationUser
        //    {
        //        Email = model.email,
        //        UserName = model.email,
        //        FullName = model.fullName,
        //        Image = model.image,
        //        Dob = model.dob,
        //        Gender = model.gender,
        //        Address = model.address,
        //        PhoneNumber = model.phoneNumber ?? string.Empty

        //    };
        //    var result = await userManager.CreateAsync(user, model.password);
        //    if (result.Succeeded)
        //    {
        //        string role = DetermineRoleFromEmail(model.email);

        //        if (role == AppRole.Patient)
        //        {
        //            var patient = new Patient
        //            {
        //                patientId = _context.Patients.Count() + 1,
        //                email = model.email
        //                // Các thuộc tính khác của bệnh nhân
        //            };
        //            _context.Patients.Add(patient);
        //        }
        //        else if (role == AppRole.Doctor)
        //        {
        //            var doctor = new Doctor
        //            {
        //                specializationName = doctorVM.specializationName,
        //                specializationId = 1,
        //                doctorId = _context.Doctors.Count() + 1,
        //                doctorName = model.fullName,
        //                doctorImage = doctorVM.doctorImg,
        //                email = doctorVM.email,
        //                degree = doctorVM.degree,
        //                experience = (double)doctorVM.experience,
        //                bookingFee = (double)doctorVM.bookingFee,
        //                doctorAbout = doctorVM.doctorAbout,
        //                isAvailable = doctorVM.isAvailable,
        //            };
        //            _context.Doctors.Add(doctor);
        //        }

        //        await _context.SaveChangesAsync();

        //        // Tạo role nếu chưa tồn tại
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //        }

        //        await userManager.AddToRoleAsync(user, AppRole.Patient);
        //    }
        //    return result;
        //}
        public async Task<IdentityResult> SignUpAsync(SignupRequest request)
        {
            var model = request.model;
            var doctorVM = request.doctorVM;
            var user = new ApplicationUser
            {
                Email = model.email,
                UserName = model.email,
                FullName = model.fullName,
                Image = model.image,
                Dob = model.dob,
                Gender = model.gender,
                Address = model.address,
                PhoneNumber = model.phoneNumber ?? string.Empty
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
                        specializationName = doctorVM.specializationName,
                        specializationId = 1,
                        doctorId = _context.Doctors.Count() + 1,
                        doctorName = model.fullName,
                        doctorImage = doctorVM.doctorImg,
                        email = doctorVM.email,
                        degree = doctorVM.degree,
                        experience = (double)doctorVM.experience,
                        bookingFee = (double)doctorVM.bookingFee,
                        doctorAbout = doctorVM.doctorAbout,
                        isAvailable = doctorVM.isAvailable,
                    };
                    _context.Doctors.Add(doctor);
                }

                await _context.SaveChangesAsync();

                // Tạo role nếu chưa tồn tại
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Thêm role dựa trên email
                await userManager.AddToRoleAsync(user, role);
            }
            return result;
        }

        private async Task<string> SaveImageToServer(IFormFile image)
        {
            // Lưu ảnh vật lý trên server và trả về đường dẫn
            // Ví dụ:
            var fileName = $"{Guid.NewGuid().ToString()}.jpg";
            var filePath = Path.Combine("wwwroot", "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return $"/images/{fileName}";
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
