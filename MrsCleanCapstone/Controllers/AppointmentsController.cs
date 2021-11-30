using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.DTOs;
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
                VehicleTypes = new List<string> { "SUV", "SEDAN", "VAN", "TRUCK" }
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
            sendGridMessage.SetFrom(configuration.GetSection("SENDGRID_MAIL").Value, "Mrs Clean Auto Spa");
            sendGridMessage.AddTo(appt.Customerfk.Email, appt.Customerfk.Name);
            sendGridMessage.SetTemplateId(configuration.GetSection("SENDGRID_TEMPLATE_ID").Value);
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
        public IActionResult Info(Guid? id)
        {
            var appointment = _repository.Get().Include(x => x.Customerfk).Include(x => x.Vehicles).SingleOrDefault(m => m.Id == id);
            if (appointment != null)
            {
                return View(appointment);
            }
            else
            {
                return RedirectToAction("Bookings", "Admin");
            }
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(AppointmentLookupDto appt)
        {
            var appointment = _repository.Get().Include(x => x.Customerfk).Include(x => x.Vehicles).SingleOrDefault(m => m.Id == appt.AppointmentId);
            if (appointment != null)
            {
                if (appointment.Customerfk.Email.Equals(appt.CustomerEmail))
                {
                    return View("InfoCustomer", appointment);
                }
                return View("InfoCustomer", null);
            }
            else
            {
                return View("InfoCustomer", null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(Appointment appointment)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Search");
            }

            if (appointment.Id != Guid.Empty)
            {
                var apptToUpdate = _repository.Get().Include(x => x.Customerfk).Include(x => x.Vehicles).SingleOrDefault(m => m.Id == appointment.Id);

                if (apptToUpdate == null)
                {
                    return new JsonResult("Error!! Appointment not found");

                }
                apptToUpdate.AnyPetHair = appointment.AnyPetHair;
                apptToUpdate.WaterHoseAvailability = appointment.WaterHoseAvailability;
                apptToUpdate.WaterHoseAvailability = appointment.WaterHoseAvailability;
                apptToUpdate.WaterSupplyConnection = appointment.WaterSupplyConnection;
                apptToUpdate.PowerOutletAvailable = appointment.PowerOutletAvailable;
                apptToUpdate.Customerfk.Name = appointment.Customerfk.Name;
                apptToUpdate.Customerfk.Email = appointment.Customerfk.Email;
                apptToUpdate.Customerfk.Address = appointment.Customerfk.Address;
                apptToUpdate.Customerfk.PhoneNumber = appointment.Customerfk.PhoneNumber;

                await _repository.Update(apptToUpdate);

                return View("InfoCustomer", apptToUpdate);

            }
            else
            {
                return RedirectToAction("Search");
            }


        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return new JsonResult("Model is invalid");
            }

            var apptToDelete = await _repository.GetByGuid((Guid)id);


            if (apptToDelete == null)
            {
                return new JsonResult("Error!! Appointment not found");
            }

            await _repository.Remove(apptToDelete);
            return new JsonResult(apptToDelete);
        }
    }
}
