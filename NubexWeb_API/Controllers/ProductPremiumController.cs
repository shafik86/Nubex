using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nubex_Business.Repository.IRepository;
using Nubex_Models;

namespace NubexWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPremiumController : Controller
    {
        public IProductPremiumRepository _productPremiumRepository { get; }
        public ProductPremiumController(IProductPremiumRepository productPremiumRepository)
        {
            _productPremiumRepository = productPremiumRepository;
        }

        [HttpGet("all/{Id}")]
        public async Task<IActionResult> GetAll(int? Id)
        {
            var result = await _productPremiumRepository.GetAll(Id);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var product = await _productPremiumRepository.GetById(Id.Value);
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
