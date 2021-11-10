using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using MrsCleanCapstone.Controllers;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MrsCleanCapstone.Data;
using System.Linq.Expressions;
using System.IO;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Test
{
    public class ProductControllerTests
    {

        [Fact]
        public async Task ProductExistsTestAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MrsCleanDatabase").Options;
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Products.AddRangeAsync(new Product
                {
                    ProductID = 101,
                    ProductName = "Cleaning Solution C",
                    Price = 35.50M,
                    Category = "CleaningSolutions",
                    Description = "Deeps cleans the inside your car"
                });
                //Arrange
                var mockRepo = new Mock<IGenericRepository<Product>>();

                var mockLogger = new Mock<ILogger<ProductsController>>();
                var mockHost = new Mock<IWebHostEnvironment>();
                int id = 101;
                ProductsController controller = new ProductsController(mockLogger.Object, mockHost.Object, mockRepo.Object);
                //Act
                var result = await context.Products.FindAsync(id); ;

                //Assert
                Assert.NotNull(result);
            }

        }

        [Fact]
        public void AddProduct_Returns_AddProductForm()
        {
            //Arrange
            var mockRepo = new Mock<IGenericRepository<Product>>();
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var mockHost = new Mock<IWebHostEnvironment>();
            ProductsController controller = new ProductsController(mockLogger.Object, mockHost.Object, mockRepo.Object);
            string expectedViewName = "Add";

            //Act
            ViewResult result = (ViewResult)controller.Add();
            string actualViewName = result.ViewName;

            //Assert
            Assert.Equal(expectedViewName, actualViewName);
        }
    }
}
