using System.Collections.Generic;
using WebStore.Domain.ViewModels.Product;

namespace WebStore.Domain.ViewModels.Catalog
{
    public class CatalogViewModel
    {
        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } 
    }
}