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
    public class FeedbackControllerTests
    {

        [Fact]
        public async void GetAllFeedbacks_Test()
        {
            //Arrange
            var feedbacks = new List<Feedback>();
            feedbacks.Add(new Feedback()
            {
                Id = 1,
                Name = "Kevin James",
                Email= "testuser@email.com",
                Message = "This is a new message"
            });
            var repoF = new Mock<IGenericRepository<Feedback>>();
            var repoV = new Mock<IGenericRepository<Vehicle>>();
            var repoC = new Mock<IGenericRepository<Customer>>();
            var repoA = new Mock<IGenericRepository<Appointment>>();
            var repoP = new Mock<IGenericRepository<Product>>();
            var repoD = new Mock<IGenericRepository<Deal>>();
            repoF.Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Feedback>>(feedbacks));
            //repo.Verify();
            var logger = new Mock<ILogger<FeedbacksController>>();
            AdminController controller = new AdminController(repoP.Object, repoA.Object, repoD.Object, repoC.Object, repoV.Object, repoF.Object);
            int expectedCount = 1;
            String expectedName = "Kevin James";
            //Act
            ViewResult result = (ViewResult)(await controller.Feedbacks());
            int actualCount = ((List<Feedback>)result.Model).Count;
            String actualCustomerName = ((List<Feedback>)result.Model)[0].Name;

            //Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedName, actualCustomerName);
        }

     
        /*
        [Fact]
        public async void UpdateFeedbacks_Test()
        {
            //Arrange
            Feedback
           
            var repoF = new Mock<IGenericRepository<Feedback>>();
            var repoV = new Mock<IGenericRepository<Vehicle>>();
            var repoC = new Mock<IGenericRepository<Customer>>();
            var repoA = new Mock<IGenericRepository<Appointment>>();
            var repoP = new Mock<IGenericRepository<Product>>();
            var repoD = new Mock<IGenericRepository<Deal>>();
            repoF.Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Feedback>>(feedbacks));
            //repo.Verify();
            var logger = new Mock<ILogger<FeedbacksController>>();
            AdminController controller = new AdminController(repoP.Object, repoA.Object, repoD.Object, repoC.Object, repoV.Object, repoF.Object);
            int expectedCount = 1;
            String expectedName = "Kevin James";
            //Act
            ViewResult result = (ViewResult)(await controller.Feedbacks());
            int actualCount = ((List<Feedback>)result.Model).Count;
            String actualCustomerName = ((List<Feedback>)result.Model)[0].Name;

            //Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedName, actualCustomerName);
        }
    }*/


}
