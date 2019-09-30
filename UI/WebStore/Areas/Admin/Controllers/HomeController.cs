using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Domain.Entities.Identity.User.RoleAdmin)]
    public class HomeController : Controller
    {
        private readonly IProductService _productData;

        public HomeController(IProductService productData) => _productData = productData;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList() => View(_productData.GetProducts(new ProductFilter()));
    }
}