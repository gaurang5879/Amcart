using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace SearchService.Domain.Entities
{
    public class ProductSearchModel
    {
        [SimpleField(IsKey = true)]
        public string Id { get; set; }

        [SearchableField]
        public string Name { get; set; }

        [SearchableField]
        public string Description { get; set; }

        [SimpleField]
        public decimal Price { get; set; }

        [SearchableField]
        public string Category { get; set; }

        [SimpleField]
        public string ImageUrl { get; set; }
    }
}
