﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore._Infrastructure.Map
{
    public static class ProductViewModelMapper
    {
        private static void CopyToProductViewModel(this Product product, ProductViewModel model)
        {
            model.Id = product.Id;
            model.Name = product.Name;
            model.Order = product.Order;
            model.ImageUrl = product.ImageUrl;
            model.Price = product.Price;
        }

        public static ProductViewModel CreateViewModel(this Product product)
        {
            var model = new ProductViewModel();
            product.CopyToProductViewModel(model);
            return model;
        }

        private static void CopyToProduct(this ProductViewModel model, Product product)
        {
            product.Name = model.Name;
            product.Order = model.Order;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
        }

        public static Product Create(this ProductViewModel model)
        {
            var product = new Product();
            model.CopyToProduct(product);
            return product;
        }
    }
}
