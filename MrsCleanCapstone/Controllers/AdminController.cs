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

namespace MrsCleanCapstone.Controllers
{
    public class AdminController : Controller
    {
        private IGenericRepository<Deal> _dealsRepository;
        private IGenericRepository<Appointment> _appointmentsRepository;
        //private IGenericRepository<Feedback> _feedbacksRepository;
        private IGenericRepository<Product> _productsRepository;
        //private IGenericRepository<Service> _servicesRepository;

        public AdminController(IGenericRepository<Product> productsRepository, IGenericRepository<Appointment> appointmentsRepository, IGenericRepository<Deal> dealsRepository)
        {
            _productsRepository = productsRepository;
            _dealsRepository = dealsRepository;
            _appointmentsRepository = appointmentsRepository;
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


        public ActionResult Appointments()
        {
            return View();
        }

        [Route("{controller}/appointments/add")]
        [HttpPost]
        public async Task<JsonResult> AddAppointmentAsync([FromBody] Appointment appt)
        {
            await _appointmentsRepository.Add(appt);

            return new JsonResult("Hello");
        }

        [Route("{controller}/allappointments/")]

        [HttpGet]
        public JsonResult GetAllAppointments()
        {
            var appointments = Json(_appointmentsRepository.Get().Include(m => m.Customerfk).Include(x => x.Vehicles).ToList());

            return appointments;
        }
    }
}
