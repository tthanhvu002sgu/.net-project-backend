using DoAn_API.Data;
using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/patients [admin]
        [HttpGet("Get-all")]
        public IActionResult GetAllPatients()
        {
            return Ok(_patientRepository.GetAll());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetPatientByEmail(string email)
        {
            var patient = await _patientRepository.GetPatientByEmailAsync(email);
            if (patient == null)
            {
                return NotFound(new { message = "Patient not found" });
            }

            return Ok(patient);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePatient([FromQuery] string email, [FromBody] ApplicationUser patient)
        {
            var result = await _patientRepository.UpdatePatient(email, patient);
            if (result)
            {
                return Ok(patient);
            }
            return NotFound(new { message = "Patient not found" });


        }

        // DELETE: api/patients/{patientId} [admin]
        [HttpDelete("{patientId}")]
        public IActionResult DeletePatient(int patientId)
        {
            _patientRepository.Delete(patientId);
            return Ok();
        }
    }
}
