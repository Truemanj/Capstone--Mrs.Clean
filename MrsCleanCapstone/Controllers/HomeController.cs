using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MrsCleanCapstone.Models.ViewModels;
using MrsCleanCapstone.GenericRepository;

namespace MrsCleanCapstone.Controllers
{
    

    public class HomeController : Controller
    {
        private readonly IGenericRepository<Product> _repository;
        public int PageSize = 4;

        public HomeController(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var homePageViewModel = new HomePageViewModel()
            {
                Feedback = new Feedback()
            };
            var message = "";
            if(!string.IsNullOrEmpty(TempData["SUCCESS"] as string))
            {
                message = TempData["SUCCESS"] as string;
            }
            if (!string.IsNullOrEmpty(TempData["ERROR"] as string))
            {
                message = TempData["ERROR"] as string;
            }
            ViewData["MESSAGE"] = message;
            return View(nameof(Index), homePageViewModel);
        }
        

        [Authorize]
        public IActionResult Privacy()
        {
            return View(nameof(Privacy));
        }

        public IActionResult Checklist()
        {
            return View(nameof(Checklist));
        }

        public IActionResult Services()
        {
            return View(nameof(Services));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult UnsuccessfulLogin()
        {
            return View();
        }
    }
}
