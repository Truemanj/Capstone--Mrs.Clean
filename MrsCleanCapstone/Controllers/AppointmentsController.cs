using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AppointmentsController(ILogger<AppointmentsController> logger, IGenericRepository<Appointment> repository)
        {
            _logger = logger;
            _repository = repository;
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

            var sendGridClient = new SendGridClient("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");

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("rishabhbajaj@icloud.com", "Mrs CLean Auto Spa");
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

    }
}
