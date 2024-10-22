using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
