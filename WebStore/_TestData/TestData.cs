using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore._TestData
{
    public static class TestData
    {
        public static List<Section> Sections { get; } = new List<Section>
        {

            new Section {Name = "Sport", Order = 0},
            new Section {Name = "Nike", Order = 0, ParentId = 1},
            new Section {Name = "Under Armour", Order = 1, ParentId = 1},
            new Section {Name = "Adidas", Order = 2, ParentId = 1},
            new Section {Name = "Puma", Order = 3, ParentId = 1},
            new Section {Name = "ASICS", Order = 4, ParentId = 1},
            new Section {Name = "Man's", Order = 1},
            new Section {Name = "Fendi", Order = 0, ParentId = 7},
            new Section {Name = "Guess", Order = 1, ParentId = 7},
            new Section {Name = "Valentino", Order = 2, ParentId = 7},
            new Section {Name = "Dior", Order = 3, ParentId = 7},
            new Section {Name = "Versace", Order = 4, ParentId = 7},
            new Section {Name = "Armani", Order = 5, ParentId = 7},
            new Section {Name = "Prada", Order = 6, ParentId = 7},
            new Section {Name = "Dolce Gabbana", Order = 7, ParentId = 7},
            new Section {Name = "Chanel", Order = 8, ParentId = 7},
            new Section {Name = "Gucci", Order = 9, ParentId = 7},
            new Section {Name = "Women's", Order = 2},
            new Section {Name = "Fendi", Order = 0, ParentId = 18},
            new Section {Name = "Guess", Order = 1, ParentId = 18},
            new Section {Name = "Valentino", Order = 2, ParentId = 18},
            new Section {Name = "Dior", Order = 3, ParentId = 18},
            new Section {Name = "Versace", Order = 4, ParentId = 18},
            new Section {Name = "Baby's", Order = 3},
            new Section {Name = "Fashion", Order = 4},
            new Section {Name = "For home", Order = 5},
            new Section {Name = "Interior", Order = 6},
            new Section {Name = "Clothing", Order = 7},
            new Section {Name = "Bags", Order = 8},
            new Section {Name = "Footwear", Order = 9}
        };

        public static List<Brand> Brands { get; } = new List<Brand>
        {
            new Brand {Name = "Acne", Order = 0},
            new Brand {Name = "Grune Erde", Order = 1},
            new Brand {Name = "Albiro", Order = 2},
            new Brand {Name = "Ronhill", Order = 3},
            new Brand {Name = "Oddmolly", Order = 4},
            new Brand {Name = "Boudestijn", Order = 5},
            new Brand {Name = "Rosch creative culture", Order = 6},
        };

        public static List<Product> Products { get; } = new List<Product>
        {
            new Product {Name = "Product 1", Price = 100, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 2", Price = 200, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 3", Price = 300, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 4", Price = 400, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 5", Price = 500, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
            new Product {Name = "Product 6", Price = 600, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 7", Price = 700, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
            new Product {Name = "Product 8", Price = 800, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
            new Product {Name = "Product 9", Price = 900, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
            new Product {Name = "Product 10", Price = 1000, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
            new Product {Name = "Product 11", Price = 1100, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
            new Product {Name = "Product 12", Price = 1200, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
        };
    }
}
