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
using Microsoft.EntityFrameworkCore;
using MrsCleanCapstone.Data;

namespace MrsCleanCapstone.Test
{
    public class HomeControllerTests
    {
        [Fact()]
        public void Index_Returns_View()
        {
            //Arrange
            var repo = new Mock<IGenericRepository<Product>>();

            HomeController controller = new HomeController(repo.Object);

            //Act
            ViewResult result = (ViewResult)controller.Index();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Index_Returns_IndexView()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Product>>();

            HomeController controller = new HomeController(repo.Object);
            string expectedViewName = "Index";

            //Act
            ViewResult result = (ViewResult)controller.Index();
            string actualViewName = result.ViewName;

            //Assert
            Assert.Equal(expectedViewName, actualViewName);
        }

        [Fact]
        public void CheckList_Returns_ChecklistView()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Product>>();
            HomeController controller = new HomeController(repo.Object);
            string expectedViewName = "Checklist";

            //Act
            ViewResult result = (ViewResult)controller.Checklist();
            string actualViewName = result.ViewName;

            //Assert
            Assert.Equal(expectedViewName, actualViewName);
        }

        [Fact]
        public void Product_Returns_ProductsView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MrsCleanDatabase").Options;

            using(var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Cleaning Solution A",
                        Quantity = 30,
                        Price = 25,
                        Category = "CleaningSolutions",
                        description = "Cleans the outside of your car"
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
                    });
            }

            
            using (var context = new ApplicationDbContext(options))
            {
                //Arrange
                var repo = new Mock<IGenericRepository<Product>>();
                repo.Setup(x => x.Get()).Returns(context.Products);
                HomeController controller = new HomeController(repo.Object);
                string expectedViewName = "Products";

                //Act
                ViewResult result = (ViewResult)controller.Products("CleaningSolutions", 1);
                
                string actualViewName = result.ViewName;

                //Assert
                Assert.Equal(expectedViewName, actualViewName);
            }
                
        }

    }
}
