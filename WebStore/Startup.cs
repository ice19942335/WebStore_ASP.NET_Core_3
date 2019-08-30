using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore._Infrastructure.Implementation;
using WebStore._Infrastructure.Interfaces;
using WebStore.DAL.Context;
using WebStore.Data;

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

            services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddTransient<WebStoreContextInitializer>();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreContextInitializer dbInitializer)
        {
            dbInitializer.InitializeAsync().Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
