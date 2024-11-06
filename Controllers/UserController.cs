using DoAn_API.Data;
using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IAccountRepository accountRepo;

        public UserController(IAccountRepository repo)
        {
            accountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignupRequest request)
        {
            var result = await accountRepo.SignUpAsync(request);
            if (result.Succeeded)
            {

                return Ok(result);
            }
            else
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            var result = await accountRepo.SignInAsync(signInVM);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
