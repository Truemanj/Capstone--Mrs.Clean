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
                return Redirect(nameof(Deals));
            }

            if (deal.Id != 0)
            {
                var dealToUpdate = await _dealsRepository.GetById(deal.Id);
                dealToUpdate.Title = deal.Title;
                dealToUpdate.Description = deal.Description;
                dealToUpdate.Highlight = deal.Title;
                return new JsonResult(dealToUpdate);
            }
            else
            {
                return new JsonResult("Error!! Deal could not be updated");
            }
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

        public IActionResult Bookings()
        {
            var appointments = _appointmentsRepository.Get().ToList();
            return View(appointments);
        }
    }
}
