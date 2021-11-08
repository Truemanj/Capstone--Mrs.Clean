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
using Microsoft.Extensions.Configuration;

namespace MrsCleanCapstone.Test
{
    public class AppointmentControllerTests
    {
        [Fact]
        public async void Book_Returns_BookView()
        {

            //Arrange
            var repo = new Mock<IGenericRepository<Appointment>>();
            var config = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<AppointmentsController>>();
            AppointmentsController controller = new AppointmentsController(logger.Object, repo.Object, config.Object);
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
                Id = Guid.NewGuid(),
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
            var config = new Mock<IConfiguration>();
            var logger = new Mock<ILogger<AppointmentsController>>();
            AppointmentsController controller = new AppointmentsController(logger.Object, repo.Object, config.Object);
            int expectedCount = 1;
            String expectedDate = "19-09-2021";
            //Act
            ViewResult result = (ViewResult)controller.Book();
            int actualCount =book.Count;
            String actualBookDate = DateTime;


            //Assert
            Assert.Equal(expectedCount, actualCount);
           // Assert.Equal(expectedDealTitle, actualBookID);

        }

        //[Fact]
        //public  void GetAppointmentById()
        //{
        //    var appointment = new Appointment()
        //    {
        //        Id=101, 
        //        AnyPetHair=true,
        //        WaterSupplyConnection=true,
        //        WaterHoseAvailability=  true,
        //        PowerOutletAvailable = false,
        //        Customerfk=new Customer()
        //        {
        //            Id=111,
        //            Name="Test User",
        //            Email = "test@email.com",
        //            PhoneNumber="29304723234",
        //            Address="123 Mississaga Rd"
        //        },
        //        Vehicles=new List<Vehicle>()
        //        {
        //            new Vehicle()
        //            {
        //                Id=222,
        //                Type="SUV",
        //                ServiceType="INTERIOR",
        //                Condition="Bad",
        //                NumSeats=5
        //            }
        //        }
        //    };
            
        //    var repo = new Mock<IGenericRepository<Appointment>>();
        //    //repo.Setup(x => x.Get()).Returns(appointment.T);
        //    //repo.Verify();
        //    var logger = new Mock<ILogger<DealsController>>();
        //    DealsController controller = new DealsController(logger.Object, repo.Object);
        //    int expectedCount = 1;
        //    String expectedDealTitle = "New Deal";
        //    //Act
        //    ViewResult result = (ViewResult)(await controller.Deals());
        //    int actualCount = ((List<Deal>)result.Model).Count;
        //    String actualDealTitle = ((List<Deal>)result.Model)[0].Title;


        //    //Assert
        //    Assert.Equal(expectedCount, actualCount);
        //    Assert.Equal(expectedDealTitle, actualDealTitle);
        //}



    }
}
