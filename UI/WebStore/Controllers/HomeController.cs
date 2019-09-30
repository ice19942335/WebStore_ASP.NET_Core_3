using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Domain.ViewModels.Catalog;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;
using WebStore.Services.Map.DTO;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productData;

        public HomeController(IProductService productData) => _productData = productData;

        public IActionResult Index()
        {

            //throw new ApplicationException("Test");

            var products = _productData.GetProducts(new ProductFilter());

            var catalogModel = new CatalogViewModel
            {
                Products = products.Select(product => product.CreateProduct().CreateViewModel())
            };

            return View(catalogModel);
        }

        public IActionResult ContactUs() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Blog() => View();

        public IActionResult Error404() => View();

        public IActionResult ErrorStatusCode(string code)
        {
            switch (code)
            {
                case "404": return RedirectToAction(nameof(Error404));
                default: return Content($"Error with code {code}");
            }
        }
    }
}