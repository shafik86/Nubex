using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nubex_Business.Repository.IRepository;
using Nubex_Common;
using Nubex_Models;

namespace NubexWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public IProductRepository _productRepository { get; }
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var product = await _productRepository.GetById(productId.Value);
            if (product == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(product);
        }


    }
}
