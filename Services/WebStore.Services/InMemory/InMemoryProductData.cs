using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Map.DTO;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => InitializationData.Sections;

        public IEnumerable<BrandDTO> GetBrands() => InitializationData.Brands.Select(brand => brand.CreateBrandDTO());

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            IEnumerable<Product> products = InitializationData.Products;

            if (filter is null) return products.Select(product => product.CreateProductDTO());

            if (filter.BrandId != null)
                products = products.Where(product => product.BrandId == filter.BrandId);

            if (filter.SectionId != null)
                products = products.Where(product => product.SectionId == filter.SectionId);

            return products.Select(product => product.CreateProductDTO());
        }

        public ProductDTO GetProductById(int id)
        {
            var product = InitializationData.Products.FirstOrDefault(product => product.Id == id);

            return product?.CreateProductDTO();
        }
    }
}