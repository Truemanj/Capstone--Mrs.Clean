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
        //private IGenericRepository<Feedback> _feedbacksRepository;
        private IGenericRepository<Product> _productsRepository;
        //private IGenericRepository<Service> _servicesRepository;
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<Vehicle> _vehicleRepository;

        public AdminController(IGenericRepository<Product> productsRepository, IGenericRepository<Appointment> appointmentsRepository, IGenericRepository<Deal> dealsRepository, IGenericRepository<Customer> customerRepository, IGenericRepository<Vehicle> vehicleRepository)
        {
            _productsRepository = productsRepository;
            _dealsRepository = dealsRepository;
            _appointmentsRepository = appointmentsRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
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
            //var products = _productsRepository.Get().ToList();
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
        //[Route("{controller}/products/edit")]

        public async Task<IActionResult> EditProduct([Bind("ProductID,ProductName,Quantity,Price,Category,Description,ProductImage")] Product product, IFormFile ProductImage)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    if (ProductImage != null)

                    {
                        if (ProductImage.Length > 0)

                        //Convert Image to byte and save to database

                        {

                            byte[] p1 = null;
                            using (var fs1 = ProductImage.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            product.ProductImage = p1;

                        }

                        await _productsRepository.Add(product);
                        return RedirectToAction("Products", "Admin");
                    }

                }
                else if (product.ProductID != 0)
                {
                    if (ProductImage != null)

                    {
                        if (ProductImage.Length > 0)

                        //Convert Image to byte and save to database

                        {

                            byte[] p1 = null;
                            using (var fs1 = ProductImage.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            product.ProductImage = p1;

                        }
                    }
                    await _productsRepository.Update(product);
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
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> EditDeal(Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Deals", "Admin");
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
                return new JsonResult("Model is invalid");
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
