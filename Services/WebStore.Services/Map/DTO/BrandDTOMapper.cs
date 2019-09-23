using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;

namespace WebStore.Services.Map.DTO
{
    public static class BrandDTOMapper
    {
        private static void CopyToBrandDTO(this Brand brand, BrandDTO dto)
        {
            dto.Id = brand.Id;
            dto.Name = brand.Name;
            dto.Order = brand.Order;
            dto.Products = brand.Products.Select(p => p.CreateProductDTO()).ToList();
        }

        public static BrandDTO CreateBrandDTO(this Brand brand)
        {
            var dto = new BrandDTO();
            brand.CopyToBrandDTO(dto);
            return dto;
        }

        private static void CopyToBrand(this BrandDTO dto, Brand brand)
        {
            brand.Id = dto.Id;
            brand.Name = dto.Name;
            brand.Order = dto.Order;
            brand.Products = dto.Products.Select(p => p.CreateProduct()).ToList();
        }

        public static Brand CreateBrand(this BrandDTO dto)
        {
            var brand = new Brand();
            dto.CopyToBrand(brand);
            return brand;
        }
    }
}
