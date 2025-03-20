using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.Infrastructure.AzureSearch
{
    public class AzureSearchService : ISearchService
    {
        private readonly SearchClient _searchClient;
        private readonly SearchIndexClient _indexClient;
        private readonly string _indexName = "products-index";

        public AzureSearchService(IConfiguration configuration)
        {
            var searchEndpoint = configuration["AzureSearch:Endpoint"];
            var searchApiKey = configuration["AzureSearch:ApiKey"];

            _indexClient = new SearchIndexClient(new Uri(searchEndpoint), new AzureKeyCredential(searchApiKey));
            _searchClient = new SearchClient(new Uri(searchEndpoint), _indexName, new AzureKeyCredential(searchApiKey));
        }

        public async Task IndexProductAsync(ProductSearchModel product)
        {
            await _searchClient.MergeOrUploadDocumentsAsync(new[] { product });
        }

        public async Task<IEnumerable<ProductSearchModel>> SearchProductsAsync(string searchText)
        {
            var results = await _searchClient.SearchAsync<ProductSearchModel>(searchText);
            return results.Value.GetResults().Select(r => r.Document);
        }
    }
}
