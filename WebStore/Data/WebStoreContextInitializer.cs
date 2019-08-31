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

        [Obsolete]
        public async Task InitializeAsync()
        {
           await _context.Database.MigrateAsync();

            if(await _context.Products.AnyAsync())
                return;


            await using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Sections.AddRange(TestData.Sections);
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            await using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Brands.AddRange(TestData.Brands);
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            await using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Products.AddRange(TestData.Products);
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }
        }

    }
}
