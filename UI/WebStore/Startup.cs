using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Clients.Employees;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Users;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Product;
using WebStore.Interfaces.Services;
using WebStore.Services;
using WebStore.Services.Data;
using WebStore.Services.InMemory;
using WebStore.Services.SQL;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddScoped<ICartService, CookieCartService>();

            services.AddIdentity<User, IdentityRole>(options => { /*Cookies configuration can be hire*/ })
                .AddDefaultTokenProviders();

            #region Identity - self-made store implementation based on WebAPI

            services.AddTransient<IUserStore<User>, UserClient>();
            services.AddTransient<IUserClaimStore<User>, UserClient>();
            services.AddTransient<IUserPasswordStore<User>, UserClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UserClient>();
            services.AddTransient<IUserEmailStore<User>, UserClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UserClient>();
            services.AddTransient<IUserLoginStore<User>, UserClient>();
            services.AddTransient<IUserLockoutStore<User>, UserClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RoleClient>();

            #endregion

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 8;
                cfg.Password.RequiredUniqueChars = 3;

                cfg.Lockout.MaxFailedAccessAttempts = 10;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                cfg.Lockout.AllowedForNewUsers = true;

                cfg.User.RequireUniqueEmail = false; //.....................................    Грабли
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.HttpOnly = true;
                cfg.ExpireTimeSpan = TimeSpan.FromDays(150); //.............................    New in Core 3
                cfg.Cookie.MaxAge = TimeSpan.FromDays(150);

                cfg.LoginPath = "/Account/Login";
                cfg.LogoutPath = "/Account/Logout";
                cfg.AccessDeniedPath = "/Account/AccessDenied";

                cfg.SlidingExpiration = true; //............................................    Change Session ID of user being authorized
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication(); //Should be after "UseRouting" middleware
            app.UseAuthorization();  //Should be after "UseRouting" middleware

            app.UseEndpoints(endpoints =>
            { 
                //endpoints.MapControllers();
                endpoints.MapAreaControllerRoute(
                    "admin",
                    "admin",
                    "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
