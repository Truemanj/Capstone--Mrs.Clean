using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MrsCleanCapstone.Controllers.Extensions;
using MrsCleanCapstone.Data;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrsCleanCapstone.Models;


namespace MrsCleanCapstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration);
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddIdentityServices(Configuration);
            services.AddMaintenance(() => false,
             Encoding.UTF8.GetBytes("<div>Doing Maintenance!</div>"));
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMaintenanceMiddleware();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    "catpage", "{category}/Page/{productPage:int}",
                    new { Controller = "Home", action = "Products" });
                
                endpoints.MapControllerRoute(
                    "page", "Products/Page/{productPage:int}",
                    new { Controller = "Home", action = "Products", productPage = 1 });

                endpoints.MapControllerRoute(
                    "category", "Products/{category}",
                    new { Controller = "Home", action = "Products" });

                endpoints.MapControllerRoute(
                    "pagination", "Products/Page/{productPage:int}",
                    new { Controller = "Home", action = "Products" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            SeedData.EnsurePopulated(app);
        }
    }
}
