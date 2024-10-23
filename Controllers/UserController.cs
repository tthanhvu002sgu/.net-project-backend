using DoAn_API.Data;
using DoAn_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

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
                data = null
            });
        }

        private string generateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return null;
        }

    }
}
