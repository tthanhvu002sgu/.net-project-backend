using DoAn_API.Models;
using DoAn_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationRepository _specializationRepository;
        public SpecializationController(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllSpecializations()
        {
            return Ok(await _specializationRepository.GetAllSpecializationsAsync());
        }
        [HttpGet("{specialization}")]
        public IActionResult GetSpecializations(string specialization)
        {
            return Ok(_specializationRepository.GetSpecializationByNameAsync(specialization));
        }
        [HttpDelete("{specialization}")]
        public IActionResult DeleteSpecializations(string specialization)
        {
            return Ok(_specializationRepository.DeleteSpecialization(specialization));
        }
        [HttpPost]
        public IActionResult AddSpecializations(SpecializationVM specialization)
        {
            return Ok(_specializationRepository.AddSpecialization(specialization));
        }
    }
}
