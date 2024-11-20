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
        [HttpGet("getallappointments")]
        public async Task<IActionResult> GetAllAppointments(int skip = 0, int pageSize = 10)
        {
            try
            {
                // Lấy danh sách lịch hẹn theo phân trang
                var appointments = await _appointmentRepository.GetAllAppointmentsAsync(skip, pageSize);

                // Đếm tổng số bản ghi (để trả về dữ liệu phân trang)
                var totalRecords = await _appointmentRepository.GetTotalAppointmentsAsync();

                // Tạo phản hồi bao gồm danh sách và metadata về phân trang
                return Ok(new
                {
                    Data = appointments,
                    Metadata = new
                    {
                        TotalRecords = totalRecords,
                        Skip = skip,
                        PageSize = pageSize,
                        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching appointments.", Error = ex.Message });
            }
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

        [HttpGet("get-booked-slots/{doctorEmail}")]
        public async Task<IActionResult> GetBookedSlotsByDoctorEmail(string doctorEmail)
        {
            if (string.IsNullOrEmpty(doctorEmail))
            {
                return BadRequest("Doctor email is required.");
            }

            var bookedSlots = await _appointmentRepository.GetBookedSlotsByDoctorEmailAsync(doctorEmail);



            return Ok(bookedSlots);
        }
        [HttpPost("check-double-booking")]
        public async Task<IActionResult> CheckDoubleBooking(string patientEmail, string date, string time)
        {


            bool isDoubleBooking = await _appointmentRepository.IsPatientDoubleBookingAsync(patientEmail, date, time);

            if (isDoubleBooking)
            {
                return Conflict(new { message = "Patient already has an appointment in the selected time slot." });
            }

            return Ok(new { message = "Time slot is available." });
        }


        [HttpPost("reject")]
        public async Task<IActionResult> RejectAppointment(
      [FromQuery] string doctorEmail,
    [FromQuery] string patientEmail,
    [FromQuery] string date,
    [FromQuery] string time,
    [FromBody] string rejectionReason)
        {
            if (string.IsNullOrEmpty(rejectionReason))
            {
                return BadRequest("Rejection reason is required.");
            }

            try
            {
                var result = await _appointmentRepository.RejectAppointmentAsync(
                    doctorEmail, patientEmail, date, time, rejectionReason);

                if (!result)
                {
                    return NotFound(new { Message = "Appointment not found." });
                }

                return Ok(new { Message = "Appointment rejected successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while rejecting the appointment.", Error = ex.Message });
            }
        }

    }
}
