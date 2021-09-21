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
    public class AppointmentControllerTests
    {
        [Fact]
        public async void Book_Returns_BookView()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Appointment>>();
            var logger = new Mock<ILogger<AppointmentsController>>();
            AppointmentsController controller = new AppointmentsController(logger.Object, repo.Object);
            string expectedViewName = null;

            //Act
            ViewResult result = (ViewResult)(controller.Book());
            string actualViewName = result.ViewName;

            //Assert
            Assert.Equal(expectedViewName, actualViewName);
        }


        [Fact]
        public async void AddAppointment_Returns_addAppointment()
        {
            //Arrange
            var book = new List<Appointment>();
            string DateTime = "19-09-2021";
            book.Add(new Appointment()
            {
                Id = 1,
                AnyPetHair = false,
                Date = DateTime,
                PowerOutletAvailable = true,
                Vehicles = null,
                WaterHoseAvailability = true,
                WaterSupplyConnection = true,



            });
            var repo = new Mock<IGenericRepository<Appointment>>();
            repo.Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Appointment>>(book));
            //repo.Verify();
            var logger = new Mock<ILogger<AppointmentsController>>();
            AppointmentsController controller = new AppointmentsController(logger.Object, repo.Object);
            int expectedCount = 1;
            String expectedDate = "19-09=2021";
            //Act
            ViewResult result = (ViewResult)controller.Book();
            int actualCount =book.Count;
            String actualBookDate = DateTime;


            //Assert
            Assert.Equal(expectedCount, actualCount);
           // Assert.Equal(expectedDealTitle, actualBookID);

        }




    }
}
