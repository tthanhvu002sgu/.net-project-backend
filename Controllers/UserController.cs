using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;
        public UserController(MyDbContext context, IOptionsMonitor<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
        }
        [HttpPost("Login")]
        public IActionResult Validate(LoginVM loginVM)
        {
            var user = _context.Users.SingleOrDefault(u => u.email == loginVM.email && u.password == loginVM.password);
            if (user == null)
            {
                return Ok(new ApiRespone
                {
                    success = false,
                    message = "Invalid username or password",

                });
            }
            //cap token
            else
            {

            }
            return Ok(new ApiRespone
            {
                success = true,
                message = "Authenticate success",
                data = generateToken(user)
            });
        }

        private string generateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]  {
                    new Claim(ClaimTypes.Name, user.fullName),

                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("Id", user.userId.ToString()),

                    //roles

                    new Claim("TokenID", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
