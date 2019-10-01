using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Order;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels.Cart;
using WebStore.Domain.ViewModels.Order;
using WebStore.Domain.ViewModels.Product;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        private CartController _controller;

        private Mock<ICartService> _cartServiceMock;
        private Mock<IOrderService> _orderServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _cartServiceMock = new Mock<ICartService>();
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new CartController(_cartServiceMock.Object, _orderServiceMock.Object);
        }

        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            _controller.ModelState.AddModelError("TestError", "Invalid Model on unit testing");

            const string expectedModeName = "Test order";

            var result = _controller.CheckOut(new OrderViewModel { Name = expectedModeName });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<DetailsViewModel>(viewResult.ViewData.Model);

            Assert.Equal(expectedModeName, model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_and_Returns_Redirect()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "1") }));

            _cartServiceMock
                .Setup(c => c.TransFromCart())
                .Returns(() => new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

            const int expectedOrderId = 1;

            _orderServiceMock
                .Setup(o => o.CreateOrder(It.IsAny<OrderModel>(), It.IsAny<string>()))
                .Returns(new OrderDTO { Id = 1 });

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = _controller.CheckOut(new OrderViewModel
            {
                Name = "Test",
                Address = "",
                Phone = ""
            });

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirectResult.ActionName);

            Assert.Equal(expectedOrderId, redirectResult.RouteValues["id"]);
        }
    }
}
