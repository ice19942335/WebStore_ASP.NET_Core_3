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
    class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, "api/products")
        {
        }

        public IEnumerable<Section> GetSections()
        {
            return Get<List<Section>>($"{_serviceAddress}/sections");
        }

        public IEnumerable<Brand> GetBrands()
        {
            return Get<List<Brand>>($"{_serviceAddress}/brands");
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
