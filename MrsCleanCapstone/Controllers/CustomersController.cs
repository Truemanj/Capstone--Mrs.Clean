using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Info(int id)
        {
            return View();
        }
    }
}
