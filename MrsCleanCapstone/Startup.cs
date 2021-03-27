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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration);
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            services.AddIdentityServices(Configuration);
            services.AddMaintenance(() => false,
             Encoding.UTF8.GetBytes("<div>Doing Maintenance Yo!</div>"));

            services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });
            services.AddScoped<InterfaceProductRepo, ProductRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    

                
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
