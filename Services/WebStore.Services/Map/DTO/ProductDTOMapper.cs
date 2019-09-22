using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities.Product;

namespace WebStore.Services.Map.DTO
{
    public static class ProductDTOMapper
    {
        private static void CopyToProductDTO(this Product product, ProductDTO dto)
        {
            dto.Id = product.Id;
            dto.Order = product.Order;
            dto.Name = product.Name;
            dto.ImageUrl = product.ImageUrl;
            dto.Price = product.Price;
            dto.Brand = product.Brand is null
                ? null
                : new BrandDTO
                {
                    Id = product.Brand.Id,
                    Name = product.Name
                };
        }

        public static ProductDTO CreateProductDTO(this Product product)
        {
            var dto = new ProductDTO();
            product.CopyToProductDTO(dto);
            return dto;
        }

        private static void CopyToProduct(this ProductDTO dto, Product product)
        {
            product.Id = dto.Id;
            product.Order = dto.Order;
            product.Name = dto.Name;
            product.ImageUrl = dto.ImageUrl;
            product.Price = dto.Price;
            product.Brand = dto.Brand
                is null ?
                null :
                new Brand
                {
                    Id = dto.Brand.Id,
                    Name = dto.Brand.Name
                };
        }

        public static Product CreateProduct(this ProductDTO dto)
        {
            var product = new Product();
            dto.CopyToProduct(product);
            return product;
        }
    }
}
