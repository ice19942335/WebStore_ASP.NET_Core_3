using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore._Infrastructure.Interfaces;
using WebStore._Infrastructure.Map;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Shop(int? sectionId, int? brandId)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId
            });

            var catalogModel = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(product => product.CreateViewModel())
            };

            return View(catalogModel);
        }
    }
}