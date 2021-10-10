using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;

namespace MrsCleanCapstone.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private IGenericRepository<Customer> _repository = null;

        public CustomersController(ILogger<CustomersController> logger, IGenericRepository<Customer> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Route("{controller}/Info/{id}")]
        public async Task<IActionResult> Info(int? id)
        {
            var customer = await _repository.GetById((int)id);

            ViewBag.Message = customer;

            return View();
        }
    }
}
