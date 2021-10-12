using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;
using MrsCleanCapstone.Models.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers
{
    public class AppointmentsController : Controller
    {

        private readonly ILogger<AppointmentsController> _logger;
        private IGenericRepository<Appointment> _repository = null;
        private IConfiguration configuration;

        public AppointmentsController(ILogger<AppointmentsController> logger, IGenericRepository<Appointment> repository, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            configuration = config;
        }

        public ActionResult Book()
        {
            var bookApptVM = new BookAppointmentViewModel()
            {
                Appointment = new Appointment(),
                Customer = new Customer(),
                Vehicle = new Vehicle(),
                ServiceTypes = new List<string> { "INTERIOR", "INTERIOR_EXTERIOR" },
                VehicleTypes = new List<string> { "SUV", "SEDAN", "VAN", "TRUCK"}
            };
            return View(bookApptVM);
        }

        [Route("{controller}/book")]
        [HttpPost]
        public async Task<JsonResult> AddAppointmentAsync([FromBody] Appointment appt)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error");
            }
            await _repository.Add(appt);

            var sendGridClient = new SendGridClient(configuration.GetSection("SENDGRID_API_KEY").Value);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(configuration.GetSection("SENDGRID_MAIL").Value, "Mrs CLean Auto Spa");
            sendGridMessage.AddTo(appt.Customerfk.Email, appt.Customerfk.Name);
            sendGridMessage.SetTemplateId("d-538ca911241f448584850d40b0ad3816");
            sendGridMessage.SetTemplateData(new ConfirmationEmail
            {
                Order = appt.Id
            });

            var response = sendGridClient.SendEmailAsync(sendGridMessage).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent");
            }

            return new JsonResult(appt);
        }


        [Route("{controller}/Info/{id}")]
        [Authorize]
        public IActionResult Info(int? id)
        {
            var appointment =  _repository.Get().Include(x=>x.Customerfk).Include(x=>x.Vehicles).SingleOrDefault(m=>m.Id==id);
            if (appointment != null)
            {
                return View(appointment);
            }
            else
            {
                return RedirectToAction("Bookings","Admin");
            }
        }

    }
}
