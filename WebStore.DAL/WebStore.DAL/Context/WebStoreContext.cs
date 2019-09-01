using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebStore.Domain.Entities;
using WebStore.Domain.Identity;


namespace WebStore.App.DAL.Context
{
    public class WebStoreContext : IdentityDbContext<User>
    {
        public  DbSet<Product> Products { get; set; }

        public  DbSet<Section> Sections { get; set; }

        public  DbSet<Brand> Brands { get; set; }

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
