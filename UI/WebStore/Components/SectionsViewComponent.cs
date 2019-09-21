using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;
        public SectionsViewComponent(IProductData productData) => _productData = productData;
        public IViewComponentResult Invoke()
        {
            var sections = GetSections();
            return View(sections);
        }

        //public async Task<IViewComponentResult> InvokeAsync() { }

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _productData.GetSections();

            var parentSections = sections
                .Where(section => section.ParentId == null)
                .Select(section => new SectionViewModel
                {
                    Id = section.Id,
                    Name = section.Name,
                    Order = section.Order,
                }).ToList();

            foreach (var parentSection in parentSections)
            {
                var childSections = sections.Where(section => section.ParentId == parentSection.Id)
                    .Select(section => new SectionViewModel
                    {
                        Id = section.Id,
                        Name = section.Name,
                        Order = section.Order
                    });

                parentSection.ChildSections.AddRange(childSections);
                parentSection.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parentSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return parentSections;
        }
    }
}
