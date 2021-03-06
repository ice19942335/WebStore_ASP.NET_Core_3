﻿using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.ViewModels.Product
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>Child sections</summary>
        public int Order { get; set; }

        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();

        /// <summary>Parent section</summary>
        public SectionViewModel ParentSections { get; set; }

    }
}
