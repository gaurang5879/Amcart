using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _repository;

        public CatalogController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetAllProductsAsync());
        }

        [HttpGet("products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("categories/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            return Ok(await _repository.GetProductsByCategoryAsync(category));
        }
    }
}
