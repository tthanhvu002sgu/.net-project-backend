using DoAn_API.Interfaces;
using DoAn_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        [HttpGet("get-by-doctor-email/{email}")]
        public async Task<IActionResult> GetAppointmentsByDoctorEmail(string email)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorEmail(email);
            return Ok(appointments);
        }
        [HttpGet("get-by-patient-email/{email}")]
        public async Task<IActionResult> GetAppointmentsByPatientEmail(string email)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPatientEmail(email);
            return Ok(appointments);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAppointment(AppointmentVM model)
        {
            try
            {
                var result = await _appointmentRepository.AddAppointmentAsync(model);

                if (!result)
                {
                    // Assuming the repository returns false when a time slot is already booked
                    return BadRequest(new { message = "The selected time slot is already booked. Please choose another time." });
                }

                return Ok(new { message = "Appointment created successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle specific argument exceptions if you have validation on input data
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors and provide details (for debugging)
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
