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

        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorRepository.GetAllDoctorsAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error retrieving doctors: {ex.Message}");
            }
        }
        [HttpPut("update-availability")]
        public async Task<IActionResult> UpdateAvailability([FromQuery] string doctorEmail, [FromQuery] bool isAvailable)
        {
            var result = await _doctorRepository.UpdateAvailabilityAsync(doctorEmail, isAvailable);
            if (result)
            {
                return Ok(new { message = "Doctor availability updated successfully." });
            }
            return NotFound(new { message = "Doctor not found." });
        }
    }


}
