using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore._Infrastructure.Map
{
    public static class SectionViewModelMapper
    {
        private static void CopyToSectionViewModel(this Section section, SectionViewModel model)
        {
            model.Id = section.Id;
            model.Name = section.Name;
            model.Order = section.Order;
        }

        public static SectionViewModel CreateViewModel(this Section section)
        {
            var model = new SectionViewModel();
            section.CopyToSectionViewModel(model);
            return model;
        }

        private static void CopyToSection(this SectionViewModel model, Section section)
        {
            section.Name = model.Name;
            section.Order = model.Order;
        }

        public static Section CreateSection(this SectionViewModel model)
        {
            var section = new Section();
            model.CopyToSection(section);
            return section;
        }
    }
}
