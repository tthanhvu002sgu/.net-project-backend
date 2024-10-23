using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_patientRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);

            }
        }
    }
}
