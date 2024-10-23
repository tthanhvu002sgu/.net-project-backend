// ﻿using DoAn_API.Models;
// using DoAn_API.Services;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// namespace DoAn_API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class LoaiController : ControllerBase
//     {
//         private readonly ITypeRepository _typeRepository;

//         public LoaiController(ITypeRepository typeRepository)
//         {
//             _typeRepository = typeRepository;

//         }

//         [HttpGet]
//         public IActionResult GetAll()
//         {
//             try
//             {
//                 return Ok(_typeRepository.GetAll());
//             }
//             catch
//             {
//                 return StatusCode(StatusCodes.Status400BadRequest);
//             }
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetById(int id)
//         {
//             try
//             {
//                 var data = _typeRepository.GetById(id);
//                 if(data != null)
//                 {
//                     return Ok(data);
//                 } 
//                 return NotFound();
//             }

//             catch
//             {
//                 return StatusCode(StatusCodes.Status400BadRequest);
//             }
//         }

//         [HttpPut]
//         public IActionResult UpdateType(int id, TypeVM typeVM)
//         {
//             if (id != typeVM.IdType)
//             {
//                 return NotFound();
//             }
//             try
//             {
//                _typeRepository.Update(typeVM);
//                return NoContent();
//             }
//             catch
//             {
//                 return StatusCode(StatusCodes.Status400BadRequest);
//             }
//         }

//         [HttpDelete]
//         public IActionResult Delete(int id)
//         {
//             try
//             {
//                 _typeRepository.Delete(id);
//                 return Ok();
//             }
//             catch
//             {
//                 return StatusCode(StatusCodes.Status400BadRequest);

//             }
//         }

//         [HttpPost]
//         public IActionResult actionResult(TypeVM typeVM)
//         {
//             try
//             {
//                 return Ok(_typeRepository.Add(typeVM));


//             }
//             catch
//             {
//                 return StatusCode(StatusCodes.Status400BadRequest);



//             }
//         }
//     }
// }
