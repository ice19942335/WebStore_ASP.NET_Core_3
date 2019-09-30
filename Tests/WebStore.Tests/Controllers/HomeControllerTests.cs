using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Services;
using Xunit.Sdk;


using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void Initialize()
        {
            var productDataMock = new Mock<IProductService>();
            _controller = new HomeController(productDataMock.Object);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var result = _controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var result = _controller.Error404();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatusCode_404_Redirect_to_Error404()
        {
            var result = _controller.ErrorStatusCode("404");
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToAction.ControllerName);
            Assert.Equal(nameof(HomeController.Error404), redirectToAction.ActionName);
        }
    }
}
