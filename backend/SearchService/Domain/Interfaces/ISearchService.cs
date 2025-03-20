using SearchService.Domain.Entities;

namespace SearchService.Domain.Interfaces
{
    public interface ISearchService
    {
        Task IndexProductAsync(ProductSearchModel product);
        Task<IEnumerable<ProductSearchModel>> SearchProductsAsync(string searchText);
    }
}
