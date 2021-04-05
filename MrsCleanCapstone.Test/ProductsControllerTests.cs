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

namespace MrsCleanCapstone.Test
{
    public class ProductControllerTests
    {
        //[Fact()]
        //public async System.Threading.Tasks.Task AddProductTestAsync()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MrsCleanDatabase").Options;

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        context.Database.EnsureDeleted();
        //        context.Products.AddRange(
        //            new Product
        //            {
        //                ProductID = 101,
        //                ProductName = "Cleaning Solution C",
        //                Quantity = 17,
        //                Price = 35.50M,
        //                Category = "CleaningSolutions",
        //                description = "Deeps cleans the inside your car"
        //            });
        //    }


        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        //Arrange
        //        //Arrange
        //        var fileMock = new Mock<IFormFile>();
        //        //Setup mock file using a memory stream
        //        var content = "Hello World from a Fake File";
        //        var fileName = "test.png";
        //        var ms = new MemoryStream();
        //        var writer = new StreamWriter(ms);
        //        writer.Write(content);
        //        writer.Flush();
        //        ms.Position = 0;
        //        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        //        fileMock.Setup(_ => _.FileName).Returns(fileName);
        //        fileMock.Setup(_ => _.Length).Returns(ms.Length);
        //        var file = fileMock.Object;

        //        var mockRepo = new Mock<IGenericRepository<Product>>();
        //        var mockLogger = new Mock<ILogger<ProductsController>>();
        //        var mockHost = new Mock<IWebHostEnvironment>();
        //        int id = 102;

        //        mockRepo.Setup(x => x.Add(new Product
        //        {
        //            ProductID = 102,
        //            ProductName = "Cleaning Solution C",
        //            Quantity = 17,
        //            Price = 35.50M,
        //            Category = "CleaningSolutions",
        //            description = "Deeps cleans the inside your car",
        //            ProductImageName = "Newimage",
        //            ProductImage = file
        //        }));
        //        ProductsController controller = new ProductsController(mockLogger.Object, mockHost.Object, mockRepo.Object);
        //        await controller.AddProduct(new Product
        //        {
        //            ProductID = 102,
        //            ProductName = "Cleaning Solution C",
        //            Quantity = 17,
        //            Price = 35.50M,
        //            Category = "CleaningSolutions",
        //            description = "Deeps cleans the inside your car",
        //            ProductImageName = "Newimage",
        //            ProductImage = file
        //        });
        //        bool expectedResult = true;

        //        //Act

        //        bool result = (bool)controller.ProductExists(id);

        //        //Assert
        //        Assert.Equal(expectedResult, result);
        //    }
        //}

        [Fact]
        public void ProductExistsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MrsCleanDatabase").Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Products.AddRange(
                    new Product
                    {
                        ProductID=101,
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
                var mockRepo = new Mock<IGenericRepository<Product>>();
                var mockLogger = new Mock<ILogger<ProductsController>>();
                var mockHost = new Mock<IWebHostEnvironment>();
                int id = 101;
                mockRepo.Setup(x => x.GetExists(e => e.ProductID == id)).Returns(true);
                ProductsController controller = new ProductsController(mockLogger.Object, mockHost.Object, mockRepo.Object);
                bool expectedResult = true;

                //Act
                
                bool result = (bool)controller.ProductExists(id);

                //Assert
                Assert.Equal(expectedResult, result);
            }

        }

    }
}
