using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;
using WebStore.Services.Map.DTO;

namespace WebStore.Services.SQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context) => _context = context;

        public IEnumerable<SectionDTO> GetSections() =>
            _context.Sections
                .Include(s => s.Products)
                .Select(section => section.CreateSectionDTO())
                .AsEnumerable();

        public IEnumerable<BrandDTO> GetBrands() =>
            _context.Brands
                .Include(b => b.Products)
                .Select(brand => brand.CreateBrandDTO())
                .AsEnumerable();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            IQueryable<Product> products = _context.Products;

            if (filter is null)
                return products.AsEnumerable().Select(p => p.CreateProductDTO());

            if (filter.SectionId != null)
                products = products.Where(product => product.SectionId == filter.SectionId);

            if (filter.BrandId != null)
                products = products.Where(product => product.BrandId == filter.BrandId);

            return products.AsEnumerable().Select(p => p.CreateProductDTO());
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _context.Products
                .Include(product => product.Brand)
                .Include(product => product.Section)
                .FirstOrDefault(product => product.Id == id);

            return product?.CreateProductDTO();
        }
    }
}
