using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrsCleanCapstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using MrsCleanCapstone.DTOs;
using MrsCleanCapstone.GenericRepository;
using System.Threading.Tasks;
using MrsCleanCapstone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using MrsCleanCapstone.Models.ViewModels;

namespace MrsCleanCapstone.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private IGenericRepository<Deal> _dealsRepository;
        private IGenericRepository<Appointment> _appointmentsRepository;
        private IGenericRepository<Feedback> _feedbackRepository;
        private IGenericRepository<Product> _productsRepository;
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<Vehicle> _vehicleRepository;

        public AdminController(IGenericRepository<Product> productsRepository, IGenericRepository<Appointment> appointmentsRepository, IGenericRepository<Deal> dealsRepository, IGenericRepository<Customer> customerRepository, IGenericRepository<Vehicle> vehicleRepository, IGenericRepository<Feedback> feedbackRepository)
        {
            _productsRepository = productsRepository;
            _dealsRepository = dealsRepository;
            _appointmentsRepository = appointmentsRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _feedbackRepository = feedbackRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Deals()
        {
            var manageDealsVM = new ManageDealsViewModel()
            {
                DealsList = _dealsRepository.Get().ToList(),
                DealData = new Deal()
            };
            return View(manageDealsVM);
        }

        public IActionResult Products()
        {
            var manageProductsVM = new ManageProductsViewModel()
            {
                ProductsList = _productsRepository.Get().ToList(),
                ProductData = new Product()
            };
            return View(manageProductsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditProduct([Bind("ProductID,ProductName,Price,Category,Description,ProductImage")] Product product, IFormFile ProductImage)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    if (ProductImage != null)

                    {
                        if (ProductImage.Length > 0)
                        {

                            byte[] imageData = null;
                            using (var fs1 = ProductImage.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                imageData = ms1.ToArray();
                            }
                            product.ProductImage = imageData;

                        }

                        await _productsRepository.Add(product);
                        return RedirectToAction("Products", "Admin");
                    }

                }
                else if (product.ProductID != 0)
                {

                    var productToUpdate = await _productsRepository.GetById(product.ProductID);
                    productToUpdate.ProductName = product.ProductName;
                    productToUpdate.Category = product.Category;
                    productToUpdate.Price = product.Price;
                    productToUpdate.Description = product.Description;
                    if (ProductImage != null)

                    {
                        if (ProductImage.Length > 0)
                        {

                            byte[] imageData = null;
                            using (var fs1 = ProductImage.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                imageData = ms1.ToArray();
                            }
                            productToUpdate.ProductImage = imageData;

                        }
                    }
                  
                    await _productsRepository.Update(productToUpdate);
                    return RedirectToAction("Products", "Admin");
                }
                else
                {
                    return RedirectToAction("Products", "Admin");

                }
                return RedirectToAction("Products", "Admin");

            }

            return RedirectToAction("Products", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditDeal([Bind]Deal deal)
        {
            if (!ModelState.IsValid)
            {
                TempData["ERROR"] = "ERROR";
                return RedirectToAction("Deals", "Admin", new { message="ERROR"});
            }

            if (deal.Id != 0)
            {
                var dealToUpdate = await _dealsRepository.GetById(deal.Id);
                if (dealToUpdate == null)
                {
                    return RedirectToAction("Deals", "Admin");

                }
                dealToUpdate.Title = deal.Title;
                dealToUpdate.Description = deal.Description;
                dealToUpdate.Highlight = deal.Highlight;
                await _dealsRepository.Update(dealToUpdate);
                return RedirectToAction("Deals", "Admin");
            }
            else if (deal.Id == 0)
            {
                await _dealsRepository.Add(deal);
                return RedirectToAction("Deals", "Admin");
            }
            else
            {
                return RedirectToAction("Deals", "Admin");
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

        [Route("{controller}/products/delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == 0)
            {
                return new JsonResult("Model is invalid");
            }

            var productToDelete = await _productsRepository.GetById((int)id);
            if (productToDelete == null)
            {
                return new JsonResult("Error!! Deal not found");
            }

            await _productsRepository.Remove(productToDelete);
            return new JsonResult(productToDelete);
        }

        public async Task<IActionResult> Feedbacks()
        {
            var feedbacksList = (await _feedbackRepository.GetAll()).ToList();
            return View(feedbacksList);
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

        [Route("{controller}/appointment/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAppointmentById(Guid? id)
        {
            var appointment = await _appointmentsRepository.Get().Include(m => m.Customerfk).Include(x => x.Vehicles).SingleOrDefaultAsync(x => x.Id == id);

            return new JsonResult(appointment);
        }

        [Route("{controller}/appointment/edit")]
        [HttpPost]
        public async Task<IActionResult> EditAppointment([FromBody] Appointment appointment)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult("Model is invalid");
            }

            if (appointment.Id != Guid.Empty)
            {
                var apptToUpdate = await _appointmentsRepository.GetByGuid(appointment.Id);
                
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
                
                await _appointmentsRepository.Update(apptToUpdate);
                
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
            Console.Write(customer);

            if (!ModelState.IsValid)
            {
                return new JsonResult("Invalid");
            }

            if (customer.Id != 0)
            {

                var custToUpdate = await _customerRepository.GetById(customer.Id);
                if (custToUpdate == null)
                {
                    return new JsonResult("Error!! Customer not found");

                }

                custToUpdate.Name = customer.Name;
                custToUpdate.PhoneNumber = customer.PhoneNumber;
                custToUpdate.Address = customer.Address;
                custToUpdate.Email = customer.Email;

                await _customerRepository.Update(custToUpdate);
                return new JsonResult(custToUpdate);


            }
            else
            {
                return new JsonResult("Error!! Customer could not be updated");
            }


        }

        [Route("{controller}/appointment/delete/{id}")]
        [HttpPost]

        public async Task<IActionResult> DeleteAppointment(Guid? id)
        {

            if (!id.HasValue)
            {
                return new JsonResult("Model is invalid");
            }

            var apptToDelete = await _appointmentsRepository.GetByGuid((Guid)id);


            if (apptToDelete == null)
            {
                return new JsonResult("Error!! Appointment not found");
            }

            await _appointmentsRepository.Remove(apptToDelete);
            return new JsonResult(apptToDelete);

        }

        [Route("{controller}/feedback/edit")]
        [HttpPost]


        public async Task<IActionResult> EditFeedback([FromBody] Feedback feedback)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult("Model is invalid");
            }

            if (feedback.Id != 0)
            {
                var fbToUpdate = await _feedbackRepository.GetById(feedback.Id);

                if (fbToUpdate == null)
                {
                    return new JsonResult("Error!! Feedback not found");

                }
                
                fbToUpdate.Name = feedback.Name;
                fbToUpdate.Email = feedback.Email;
                fbToUpdate.Message = feedback.Message;

                await _feedbackRepository.Update(fbToUpdate);

                return new JsonResult(fbToUpdate);

            }
            else
            {
                return new JsonResult("Error!! Appointment could not be updated");
            }


        }

        [Route("{controller}/feedback/delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteFeedback(int? id)
        {

            if (id == 0)
            {
                return new JsonResult("Model is invalid");
            }

            var fbToDelete = await _feedbackRepository.GetById((int)id);


            if (fbToDelete == null)
            {
                return new JsonResult("Error!! Feedback not found");
            }

            await _feedbackRepository.Remove(fbToDelete);
            return new JsonResult(fbToDelete);

        }


    }
}
