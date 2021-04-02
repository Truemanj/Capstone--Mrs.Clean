using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using MrsCleanCapstone.Models;

namespace MrsCleanCapstone.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                                        .CreateScope().ServiceProvider
                                        .GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Cleaning Solution A",
                        Quantity = 10,
                        Price = 25,
                        Category = "CleaningSolutions",
                        description = "Cleans the outside of your car",
                        ProductImageName="1.jpg"
                    },
                    new Product
                    {
                        ProductName = "Cleaning Solution B",
                        Quantity = 40,
                        Price = 20,
                        Category = "CleaningSolutions",
                        description = "Cleans the interior of your car"
                    },
                    new Product
                    {
                        ProductName = "Cleaning Solution C",
                        Quantity = 17,
                        Price = 35.50M,
                        Category = "CleaningSolutions",
                        description = "Deeps cleans the inside your car"
                    },
                    new Product
                    {
                        ProductName = "Fleece Shammy",
                        Quantity = 100,
                        Price = 10,
                        Category = "CleaningTools",
                        description = "Helps give your car a nice shine"
                    },
                    new Product
                    {
                        ProductName = "Brush and Bucket",
                        Quantity = 55,
                        Price = 15.45M,
                        Category = "CleaningTools",
                        description = "Helps get suds all around your car"
                    },
                   new Product
                   {
                       ProductName = "Air Freshener",
                       Quantity = 200,
                       Price = 5,
                       Category = "Fresheners",
                       description = "Leaves a refreshing scent"
                   });
                
                context.SaveChanges();
            }
        }
    }
}
