using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Order;
using WebStore.Domain.Entities.Product;

namespace WebStore.DAL.Context
{
    public class WebStoreContext : IdentityDbContext<User>
    {
        public  DbSet<Product> Products { get; set; }

        public  DbSet<Section> Sections { get; set; }

        public  DbSet<Brand> Brands { get; set; }

        public  DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            : base(options)
        {

        }

        //Fluent API
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Section>()
        //        .HasMany(section => section.Products)
        //        .WithOne(product => product.Section)
        //        .HasForeignKey(product => product.SectionId);
        //}
    }
}
