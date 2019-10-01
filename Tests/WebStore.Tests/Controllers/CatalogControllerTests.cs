using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;
using WebStore.Domain.ViewModels.Catalog;
using WebStore.Domain.ViewModels.Product;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        //[Description("Test description")]
        //[Timeout(700)]
        //[Priority(1)]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            const int expectedId = 1;
            var expectedName = $"Item id {expectedId}";
            var expectedPrice = 10m;
            var expectedBrandName = $"Brand of item id {expectedId}";

            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Name = $"Item id {id}",
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 0,
                    Price = expectedPrice,
                    Brand = new BrandDTO { Id = 1, Name = $"Brand of item id {id}" }
                });

            var controller = new CatalogController(productServiceMock.Object);

            var result = controller.ProductDetails(expectedId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);

            Assert.Equal(expectedId, model.Id);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(expectedPrice, model.Price);
            Assert.Equal(expectedBrandName, model.Brand);
        }

        [TestMethod]
        public void ProductDetails_Returns_NotFound()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns(default(ProductDTO));

            var controller = new CatalogController(productServiceMock.Object);

            var result = controller.ProductDetails(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [TestMethod]
        public void Shop_Returns_Correct_View()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns<ProductFilter>(filter => new[]
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Product 1",
                        Order = 0,
                        ImageUrl = "Image_id_1.png",
                        Price = 10m,
                        Brand = new BrandDTO{ Id = 1, Name = "Brand of product 1"}
                    },
                    new ProductDTO
                    {
                        Id = 2,
                        Name = "Product 2",
                        Order = 0,
                        ImageUrl = "Image_id_2.png",
                        Price = 10m,
                        Brand = new BrandDTO{ Id = 1, Name = "Brand of product 2"}
                    },
                    new ProductDTO
                    {
                        Id = 3,
                        Name = "Product 3",
                        Order = 0,
                        ImageUrl = "Image_id_3.png",
                        Price = 10m,
                        Brand = new BrandDTO{ Id = 1, Name = "Brand of product 3"}
                    }
                });

            var controller = new CatalogController(productServiceMock.Object);

            const int expectedSectionId = 1;
            const int expectedBrandId = 5;

            var result = controller.Shop(expectedSectionId, expectedBrandId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Products.Count());
            Assert.Equal(expectedBrandId, model.BrandId);
            Assert.Equal(expectedSectionId, model.SectionId);

            Assert.Equal("Brand of product 1", model.Products.First().Brand);
        }
    }
}
