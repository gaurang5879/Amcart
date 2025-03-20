using Microsoft.AspNetCore.Mvc;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexProduct([FromBody] ProductSearchModel product)
        {
            await _searchService.IndexProductAsync(product);
            return Ok("Product indexed successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSearchModel>>> SearchProducts([FromQuery] string query)
        {
            var results = await _searchService.SearchProductsAsync(query);
            return Ok(results);
        }
    }
}
