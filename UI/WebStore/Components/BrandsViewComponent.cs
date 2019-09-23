using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels.Product;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;
using WebStore.Services.Map.DTO;
using WebStore.Services.Map.ViewModel;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData) => _productData = productData;


        public IViewComponentResult Invoke()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _productData.GetBrands();

            return brands.Select(brand => brand.CreateBrand())
                .Select(brand => brand.CreateViewModel());
        }
    }
}
