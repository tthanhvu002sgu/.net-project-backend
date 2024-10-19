//using DoAn_API.Data;
//using DoAn_API.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Type = DoAn_API.Data.Type;

//namespace DoAn_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TypeController : ControllerBase
//    {
//        private readonly MyDbContext _context;

//        public TypeController(MyDbContext context)
//        {
//            _context = context;

//        }

//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var types = _context.Types.ToList();
//            return Ok(types);
//        }

//        [HttpPost]
//        public IActionResult AddType(TypeModel model)
//        {
//            try
//            {
//                var type = new Type { NameType = model.NameType };
//                _context.Types.Add(type);
//                _context.SaveChanges();
//                return Ok(type);
//            }
//            catch
//            {
//                return BadRequest();
//            }


//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateType(int id, TypeModel model)
//        {
//            try
//            {
//                var type = _context.Types.SingleOrDefault(type => type.IdType == id);
//                if(type != null)
//                {
//                    type.NameType = model.NameType;
//                    _context.SaveChanges();
//                    return Ok();    
//                }
//                else
//                {
//                    return NotFound();
//                }
                
//            }
//            catch
//            {
//                return BadRequest();
//            }

//        }
//    }
//}
