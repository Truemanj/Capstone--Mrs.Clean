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
        private IGenericRepository<Appointment> _repository = null;

        public IActionResult Index()
        {
            return View();
        }
    }
}
