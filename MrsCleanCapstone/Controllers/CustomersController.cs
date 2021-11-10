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
        private IGenericRepository<Customer> _customerRepository = null;
        private IGenericRepository<Appointment> _appointmentsRepository = null;

        public CustomersController(ILogger<CustomersController> logger, IGenericRepository<Customer> customerRepository, IGenericRepository<Appointment> appointmentsRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _appointmentsRepository = appointmentsRepository;
        }

    }
}
