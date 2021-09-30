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
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Test
{
    public class DealsControllerTests
    {

        [Fact]
        public async void Deals_Returns_DealsView()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Deal>>();
            var logger = new Mock<ILogger<DealsController>>();
            DealsController controller = new DealsController(logger.Object, repo.Object);
            string expectedViewName = "Deals";

            //Act
            ViewResult result = (ViewResult)(await controller.Deals());
            string actualViewName = result.ViewName;

            //Assert
            Assert.Equal(expectedViewName, actualViewName);
        }

        [Fact]
        public async void Deals_Returns_EmptyModel()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Deal>>();
            var logger = new Mock<ILogger<DealsController>>();
            DealsController controller = new DealsController(logger.Object, repo.Object);
            int expectedCount = 0;

            //Act
            ViewResult result = (ViewResult)(await controller.Deals());
            int actualCount = ((List<Deal>)result.Model).Count;


            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async void Deals_Returns_DealsList()
        {
            //Arrange
            var deals = new List<Deal>();
            deals.Add(new Deal()
            {
                Id = 1,
                Title = "New Deal",
                Description = "New Deal",
                Highlight = "New Deal"
            });
            var repo = new Mock<IGenericRepository<Deal>>();
            repo.Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Deal>>(deals));
            //repo.Verify();
            var logger = new Mock<ILogger<DealsController>>();
            DealsController controller = new DealsController(logger.Object, repo.Object);
            int expectedCount = 1;
            String expectedDealTitle = "New Deal";
            //Act
            ViewResult result = (ViewResult)(await controller.Deals());
            int actualCount = ((List<Deal>)result.Model).Count;
            String actualDealTitle = ((List<Deal>)result.Model)[0].Title;


            //Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedDealTitle, actualDealTitle);
        }
    }
}
