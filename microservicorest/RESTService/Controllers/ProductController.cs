using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.Models;
using ProductMicroservice.Repository;
using System.Threading.Tasks;

namespace ProductMicroservice.Controllers
{
    [Route("/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult PostAsync([FromBody] Product product)
        {
             _productRepository.PostAsync(product);
            return Ok(product);
        }
    }
}