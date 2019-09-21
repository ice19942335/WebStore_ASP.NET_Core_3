using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    /// <summary>Product service</summary>
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter filter);

        Product GetProductById(int id);
    }
}
