﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore._Infrastructure.Interfaces;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData) => _productData = productData;

        public IActionResult Index(int? sectionId, int? brandId)
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
                Products = products.Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Order = product.Order,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                })
            };

            return View(catalogModel);
        }

        public IActionResult ProductDetails() => View();

        public IActionResult Login() => View();

        public IActionResult ContactUs() => View();

        public IActionResult CheckOut() => View();

        public IActionResult Cart() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Blog() => View();

        public IActionResult Error404() => View();
    }
}