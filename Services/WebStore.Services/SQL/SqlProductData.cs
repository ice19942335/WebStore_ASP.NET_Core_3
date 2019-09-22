using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.SQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context) => _context = context;

        public IEnumerable<Section> GetSections() =>
            _context.Sections
                .Include(s => s.Products)
                .AsEnumerable();

        public IEnumerable<Brand> GetBrands() =>
            _context.Brands
                .Include(b => b.Products)
                .AsEnumerable();

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            IQueryable<Product> products = _context.Products;

            if (filter is null)
                return products;

            if (filter.SectionId != null)
                products = products.Where(product => product.SectionId == filter.SectionId);

            if (filter.BrandId != null)
                products = products.Where(product => product.BrandId == filter.BrandId);

            return products.AsEnumerable();
        }

        public Product GetProductById(int id) => 
            _context.Products
                .Include(product => product.Brand)
                .Include(product => product.Section)
                .FirstOrDefault(product => product.Id == id);
    }
}
