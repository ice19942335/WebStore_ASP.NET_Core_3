using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Domain.ViewModels.Product;

namespace WebStore.Services.Map
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
            model.Brand = product.Brand?.Name;
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

        public static Product CreateProduct(this ProductViewModel model)
        {
            var product = new Product();
            model.CopyToProduct(product);
            return product;
        }
    }
}
