﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.App.DAL.Context;

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
                _context.Sections.AddRange(InitializationData.Sections);
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Brands.AddRange(InitializationData.Brands);
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Products.AddRange(InitializationData.Products);
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }
        }

    }
}
