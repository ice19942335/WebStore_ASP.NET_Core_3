using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreContextInitializer
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public WebStoreContextInitializer(WebStoreContext ctx, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
           await _context.Database.MigrateAsync();

           await InitializeIdentity();

            if (await _context.Products.AnyAsync())
                return;


            await using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Sections.AddRangeAsync(InitializationData.Sections);
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                await transaction.CommitAsync();
            }

            await using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Brands.AddRangeAsync(InitializationData.Brands);
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                await transaction.CommitAsync();
            }

            await using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Products.AddRangeAsync(InitializationData.Products);
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                await transaction.CommitAsync();
            }
        }

        private async Task InitializeIdentity()
        {
            if (!await _roleManager.RoleExistsAsync(User.RoleUser))
                await _roleManager.CreateAsync(new IdentityRole(User.RoleUser));

            if (!await _roleManager.RoleExistsAsync(User.RoleAdmin))
                await _roleManager.CreateAsync(new IdentityRole(User.RoleAdmin));

            if (await _userManager.FindByEmailAsync(User.AdminUserName) == null)
            {
                var admin = new User
                {
                    UserName = User.AdminUserName,
                    Email = $"{User.AdminUserName}@server.com"
                };

                var creationResult = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);

                if (creationResult.Succeeded)
                    await _userManager.AddToRoleAsync(admin, User.RoleAdmin);
            }
        }
    }
}
