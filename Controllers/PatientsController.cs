using DoAn_API.Data;
using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize(Roles = AppRole.Admin)]
        public IActionResult GetAllPatients()
        {
            return Ok(_patientRepository.GetAll());
        }

        // GET: api/patients/{patientId} [patient, admin]
        [HttpGet("{patientId}")]
        public IActionResult GetPatientById(int patientId)
        {
            var patient = _patientRepository.GetPatientVM(patientId);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        // PUT: api/patients/{patientId} [patient, admin]
        [HttpPut("{patientId}")]
        public IActionResult UpdatePatient(int id, PatientVM patient)
        {
            if (id.CompareTo(patient.patientId) != 0)
                return BadRequest();

            _patientRepository.Update(patient);
            return NoContent();
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
