using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;

namespace WebStore.Services.Map.DTO
{
    public static class SectionDTOMapper
    {
        private static void CopyToSectionDTO(this Section section, SectionDTO dto)
        {
            dto.Id = section.Id;
            dto.Name = section.Name;
            dto.Order = section.Order;
            dto.ParentId = section.ParentId;
            dto.ParentSection = section.ParentSection?.CreateSectionDTO();
            dto.Products = section.Products?.Select(product => product.CreateProductDTO()).ToList();
        }

        public static SectionDTO CreateSectionDTO(this Section section)
        {
            var dto = new SectionDTO();
            section.CopyToSectionDTO(dto);
            return dto;
        }

        private static void CopyToSection(this SectionDTO dto, Section section)
        {
            section.Id = dto.Id;
            section.Name = dto.Name;
            section.Order = dto.Order;
            section.ParentId = dto.ParentId;
            section.ParentSection = dto.ParentSection?.CreateSection();
            section.Products = dto.Products?.Select(product => product.CreateProduct()).ToList();
        }

        public static Section CreateSection(this SectionDTO dto)
        {
            var section = new Section();
            dto.CopyToSection(section);
            return section;
        }
    }
}
