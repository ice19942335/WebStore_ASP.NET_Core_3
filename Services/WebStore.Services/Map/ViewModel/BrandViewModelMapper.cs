using WebStore.Domain.Entities.Product;
using WebStore.Domain.ViewModels.Product;

namespace WebStore.Services.Map.ViewModel
{
    public static class BrandViewModelMapper
    {
        private static void CopyToBrandViewModel(this Brand brand, BrandViewModel model)
        {
            model.Id = brand.Id;
            model.Name = brand.Name;
            model.Order = brand.Order;
        }

        public static BrandViewModel CreateViewModel(this Brand brand)
        {
            var model = new BrandViewModel();
            brand.CopyToBrandViewModel(model);
            return model;
        }

        private static void CopyToBrand(this BrandViewModel model, Brand brand)
        {
            brand.Name = model.Name;
            brand.Order = model.Order;
        }

        public static Brand CreateBrand(this BrandViewModel model)
        {
            var brand = new Brand();
            model.CopyToBrand(brand);
            return brand;
        }
    }
}
