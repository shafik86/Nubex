using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nubex_Business.Repository.IRepository;
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


        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                    return BadRequest();

                var createdProduct = await _productRepository.Create(productDTO);

                return CreatedAtAction(nameof(Get),
                    new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new product record" + e.Message);

            }
        }
    }
}
