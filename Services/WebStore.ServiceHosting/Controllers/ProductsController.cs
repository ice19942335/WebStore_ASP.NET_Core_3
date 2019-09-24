using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData) => _productData = productData;

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDTO GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }
    }
}