using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MrsCleanCapstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var server = Configuration["DbServer"]??"localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "Mrsclean@capstone";
            var database = Configuration["Database"] ?? "MrsCleancapstone";

            Console.WriteLine(server + "\n" + port + "\n" + user + "\n" + password + "\n" + database);

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer($"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}"));

            //services.AddScoped<ITokenService, TokenService>();

            return services;

        }
    }
}
