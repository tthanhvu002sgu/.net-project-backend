using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DoAn_API.Services;
using DoAn_API.Models;

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
        public IActionResult UpdatePatient(int patientId, PatientVM patient)
        {
            if (patientId != patient.userId)
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
