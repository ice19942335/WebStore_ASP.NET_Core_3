using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore._TestData;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreContextInitializer
    {
        private readonly WebStoreContext _context;

        public WebStoreContextInitializer(WebStoreContext ctx) => _context = ctx;

        public async Task InitializeAsync()
        {
           await _context.Database.MigrateAsync();

            if(await _context.Products.AnyAsync())
                return;


            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Sections.AddRange(TestData.Sections);
                _context.SaveChanges();
                transaction.Commit();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Brands.AddRange(TestData.Brands);
                _context.SaveChanges();
                transaction.Commit();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Products.AddRange(TestData.Products);
                _context.SaveChanges();
                transaction.Commit();
            }
        }

    }
}
