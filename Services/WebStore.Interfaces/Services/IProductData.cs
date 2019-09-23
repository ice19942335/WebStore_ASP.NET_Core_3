using System.Collections.Generic;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;

namespace WebStore.Interfaces.Services
{
    /// <summary>Product service</summary>
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<BrandDTO> GetBrands();

        IEnumerable<ProductDTO> GetProducts(ProductFilter filter);

        ProductDTO GetProductById(int id);
    }
}
