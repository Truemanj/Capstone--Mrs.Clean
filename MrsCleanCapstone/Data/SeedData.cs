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
                        Price = 25,
                        Category = "CleaningSolutions",
                        Description = "Cleans the outside of your car"
                    },
                    new Product
                    {
                        ProductName = "Cleaning Solution B",
                        Price = 20,
                        Category = "CleaningSolutions",
                        Description = "Cleans the interior of your car"
                    },
                    new Product
                    {
                        ProductName = "Cleaning Solution C",
                        Price = 35.50M,
                        Category = "CleaningSolutions",
                        Description = "Deeps cleans the inside your car"
                    },
                    new Product
                    {
                        ProductName = "Fleece Shammy",
                        Price = 10,
                        Category = "CleaningTools",
                        Description = "Helps give your car a nice shine"
                    },
                    new Product
                    {
                        ProductName = "Brush and Bucket",
                        Price = 15.45M,
                        Category = "CleaningTools",
                        Description = "Helps get suds all around your car"
                    },
                   new Product
                   {
                       ProductName = "Air Freshener",
                       Price = 5,
                       Category = "Fresheners",
                       Description = "Leaves a refreshing scent"
                   });
                context.SaveChanges();
            }
            if (!context.Deals.Any())
            {
                context.Deals.AddRange(
                    new Deal
                    {
                        
                        Title = "Deal A",
                        Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Delectus fugit iusto ratione.",
                        Highlight = "10% OFF",

                    },
                    new Deal
                    {
                        
                        Title = "Deal B",
                        Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                        Highlight = "20% OFF",
                    },
                    new Deal
                    {

                        Title = "Deal C",
                        Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit.",
                        Highlight = "50% OFF",
                    });

                context.SaveChanges();

            }
        }
    }
}
