using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller

    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        }
        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            return Ok(_doctorRepository.GetAllDoctorsAsync());
        }
    }
}
