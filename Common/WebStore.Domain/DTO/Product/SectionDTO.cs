using System.Collections.Generic;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using WebStore.Domain.Entities.Product;

namespace WebStore.Domain.DTO.Product
{
    public class SectionDTO : BaseEntity, INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }

        public SectionDTO ParentSection { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
