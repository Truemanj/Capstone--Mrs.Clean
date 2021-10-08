using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrsCleanCapstone.Data;
// using MrsCleanCapstone.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using MrsCleanCapstone.DTOs;
using MrsCleanCapstone.GenericRepository;
using System.Threading.Tasks;
using MrsCleanCapstone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MrsCleanCapstone.Controllers
{

    [Authorize]

    public class AdminController : Controller
    {
        private IGenericRepository<Deal> _dealsRepository;
        private IGenericRepository<Appointment> _appointmentsRepository;
        //private IGenericRepository<Feedback> _feedbacksRepository;
        private IGenericRepository<Product> _productsRepository;
        //private IGenericRepository<Service> _servicesRepository;
        private IGenericRepository<Customer> _customerRepository;

        public AdminController(IGenericRepository<Product> productsRepository, IGenericRepository<Appointment> appointmentsRepository, IGenericRepository<Deal> dealsRepository, IGenericRepository<Customer> customerRepository)
        {
            _productsRepository = productsRepository;
            _dealsRepository = dealsRepository;
            _appointmentsRepository = appointmentsRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Deals()
        {
            var deals = _dealsRepository.Get().ToList();
            return View(deals);
        }

        [Route("{controller}/deals/edit")]
        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> EditDeal([FromBody] Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Model is invalid");
            }

            if (deal.Id != 0)
            {
                var dealToUpdate = await _dealsRepository.GetById(deal.Id);
                if (dealToUpdate == null)
                {
                    return new JsonResult("Error!! Deal not found");

                }
                dealToUpdate.Title = deal.Title;
                dealToUpdate.Description = deal.Description;
                dealToUpdate.Highlight = deal.Highlight;
                await _dealsRepository.Update(dealToUpdate);
                return new JsonResult(dealToUpdate);
            }
            else
            {
                return new JsonResult("Error!! Deal could not be updated");
            }
        }

        [Route("{controller}/deals/delete/{id}")]
        [HttpPost]

        public async Task<IActionResult> DeleteDeal(int? id)
        {
            if (id == 0)
            {
                return new JsonResult("Model is invalid");
            }

            var dealToDelete = await _dealsRepository.GetById((int)id);
            if (dealToDelete == null)
            {
                return new JsonResult("Error!! Deal not found");
            }

            await _dealsRepository.Remove(dealToDelete);
            return new JsonResult(dealToDelete);

        }


        public IActionResult Products()
        {
            var products = _productsRepository.Get().ToList();
            return View(products);
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Feedbacks()
        {
            return View();
        }


        public ActionResult Bookings()
        {
            return View();
        }

       

        [Route("{controller}/allappointments/")]

        [HttpGet]
        public JsonResult GetAllAppointments()
        {
            var appointments = Json(_appointmentsRepository.Get().Include(m => m.Customerfk).Include(x => x.Vehicles).ToList());

            return appointments;
        }

        [Route("{controller}/appointment/edit")]
        [HttpPost]
        

        public async Task<IActionResult> EditAppointment([FromBody] Appointment appointment)
        {
            
            
            
            
            if (!ModelState.IsValid)
            {
                return new JsonResult("Model is invalid");
            }

            if (appointment.Id != 0)
            {
                var apptToUpdate = await _appointmentsRepository.GetById(appointment.Id);
               // var custToUpdate = await _customerRepository.GetById(customer.Id);
                if (apptToUpdate == null)
                {
                    return new JsonResult("Error!! Appointment not found");

                }
                apptToUpdate.Date = appointment.Date;
                apptToUpdate.AnyPetHair = appointment.AnyPetHair;
                apptToUpdate.WaterHoseAvailability = appointment.WaterHoseAvailability;
                apptToUpdate.WaterHoseAvailability = appointment.WaterHoseAvailability;
                apptToUpdate.WaterSupplyConnection = appointment.WaterSupplyConnection;
                apptToUpdate.PowerOutletAvailable = appointment.PowerOutletAvailable;
                //custToUpdate.PhoneNumber = customer.PhoneNumber;
                //custToUpdate.Address = customer.Address;
                //custToUpdate.Email = customer.Email;
                await _appointmentsRepository.Update(apptToUpdate);
                //await _customerRepository.Update(custToUpdate);
                return new JsonResult(apptToUpdate);


            }
            else
            {
                return new JsonResult("Error!! Appointment could not be updated");
            }


        }

        [Route("{controller}/customer/edit")]
        [HttpPost]


        public async Task<IActionResult> EditCustomer([FromBody] Customer customer)
        {




            if (!ModelState.IsValid)
            {
                return new JsonResult("Model is invalid");
            }

            if (customer.Id != 0)
            {
                
                var custToUpdate = await _customerRepository.GetById(customer.Id);
                if (custToUpdate == null)
                {
                    return new JsonResult("Error!! Customer not found");

                }
                
                custToUpdate.PhoneNumber = customer.PhoneNumber;
                custToUpdate.Address = customer.Address;
                custToUpdate.Email = customer.Email;
                
                await _customerRepository.Update(custToUpdate);
                return new JsonResult(custToUpdate);


            }
            else
            {
                return new JsonResult("Error!! Appointment could not be updated");
            }


        }

        [Route("{controller}/appointment/delete/{id}")]
        [HttpPost]

        public async Task<IActionResult> DeleteAppointment(int? id)
        {
            if (id == 0)
            {
                return new JsonResult("Model is invalid");
            }

            var apptToDelete = await _appointmentsRepository.GetById((int)id);
            if (apptToDelete == null)
            {
                return new JsonResult("Error!! Appointment not found");
            }

            await _appointmentsRepository.Remove(apptToDelete);
            return new JsonResult(apptToDelete);

        }

       
    }
}
