using Microsoft.AspNetCore.Mvc;
using DoAn_API.Models;
using DoAn_API.Services;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationRepository;

        public SpecializationController(ISpecializationService specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        // GET: api/specialization
        [HttpGet]
        public ActionResult<List<SpecializationVM>> GetAllSpecializations()
        {
            var specializations = _specializationRepository.GetAll();
            return Ok(specializations);
        }

        // GET: api/specialization/5
        [HttpGet("{id}")]
        public ActionResult<SpecializationVM> GetSpecialization(int id)
        {
            var specialization = _specializationRepository.GetSpecializationVM(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }

        // POST: api/specialization
        [HttpPost]
        public ActionResult<SpecializationVM> CreateSpecialization([FromBody] SpecializationVM specializationVM)
        {
            if (specializationVM == null)
            {
                return BadRequest("Specialization data is null.");
            }

            var createdSpecialization = _specializationRepository.Add(specializationVM);
            return CreatedAtAction(nameof(GetSpecialization), new { id = createdSpecialization.specializationId }, createdSpecialization);
        }

        // PUT: api/specialization/5
        [HttpPut("{id}")]
        public IActionResult UpdateSpecialization(int id, [FromBody] SpecializationVM specializationVM)
        {
            if (id != int.Parse(specializationVM.specializationId))
            {
                return BadRequest("ID mismatch.");
            }

            var existingSpecialization = _specializationRepository.GetSpecializationVM(id);
            if (existingSpecialization == null)
            {
                return NotFound();
            }

            _specializationRepository.Update(specializationVM);
            return NoContent(); // 204 No Content
        }

        // DELETE: api/specialization/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSpecialization(int id)
        {
            var specialization = _specializationRepository.GetSpecializationVM(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationRepository.Delete(id);
            return NoContent(); // 204 No Content
        }
    }
}
