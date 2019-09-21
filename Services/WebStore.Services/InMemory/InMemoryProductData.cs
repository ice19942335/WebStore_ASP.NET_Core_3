using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => InitializationData.Sections;

        public IEnumerable<Brand> GetBrands() => InitializationData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            IEnumerable<Product> products = InitializationData.Products;

            if (filter is null) return products;

            if (filter.BrandId != null)
                products = products.Where(product => product.BrandId == filter.BrandId);

            if (filter.SectionId != null)
                products = products.Where(product => product.SectionId == filter.SectionId);

            return products;
        }

        public Product GetProductById(int id) => InitializationData.Products.FirstOrDefault(product => product.Id == id);
    }
}