using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, "api/products")
        {
        }

        public IEnumerable<SectionDTO> GetSections()
        {
            return Get<List<SectionDTO>>($"{_serviceAddress}/sections");
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return Get<List<BrandDTO>>($"{_serviceAddress}/brands");
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            var response = Post(_serviceAddress, filter);
            return response.Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;
        }

        public ProductDTO GetProductById(int id)
        {
            return Get<ProductDTO>($"{_serviceAddress}/{id}");
        }
    }
}
