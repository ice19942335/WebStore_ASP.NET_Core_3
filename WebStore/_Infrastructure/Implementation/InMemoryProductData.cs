using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore._Infrastructure.Interfaces;
using WebStore._TestData;
using WebStore.Domain.Entities;

namespace WebStore._Infrastructure.Implementation
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            IEnumerable<Product> products = TestData.Products;

            if (filter is null) return products;

            if (filter.BrandId != null)
                products = products.Where(product => product.BrandId == filter.BrandId);

            if (filter.SectionId != null)
                products = products.Where(product => product.SectionId == filter.SectionId);

            return products;
        }
    }
}