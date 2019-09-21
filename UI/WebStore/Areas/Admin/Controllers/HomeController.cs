using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore._Infrastructure.Interfaces;
using WebStore.Domain.Entities;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Domain.Entities.Identity.User.RoleAdmin)]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData) => _productData = productData;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList() => View(_productData.GetProducts(new ProductFilter()));
    }
}