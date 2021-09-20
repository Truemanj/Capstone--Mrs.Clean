using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;
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

        public AppointmentsController(ILogger<AppointmentsController> logger,  IGenericRepository<Appointment> repository)
        {
            _logger = logger;
            _repository = repository;
        }        

        public ActionResult Book()
        {
            Appointment appt = new Appointment();
            return View(appt);
        }

    }
}
