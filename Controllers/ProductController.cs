using DoAn_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<product> products = new List<product>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }
        [HttpGet("{name}")]
        public IActionResult GetProductByName(string name)
        {
            try
            {
                var pd = products.FirstOrDefault(x => x.Name == name);
                if (pd != null)
                {
                    return Ok(pd);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]

        public IActionResult AddProduct(product pd)
        {
            var product = new product
            {
                Id = Guid.NewGuid(),
                Name = pd.Name,
                Price = pd.Price
            };
            products.Add(product);
            return Ok();
        }

        [HttpPut("{name}")]

        public IActionResult UpdateProduct(string name,product pd)
    
        {
            try
            {
                var product = products.FirstOrDefault(x => x.Name == name);
                if (product == null)
                {
                    return NotFound();
                }
                
                product.Name = pd.Name;
                product.Price = pd.Price;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteProduct(string name)
        {
            try
            {
                var product = products.FirstOrDefault(x => x.Name == name);
                if (product == null)
                {
                    return NotFound();
                }
                products.Remove(product);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
