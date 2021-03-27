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

namespace MrsCleanCapstone.Controllers
{
    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private InterfaceProductRepo repository;
        public int PageSize = 4;


        public HomeController(ILogger<HomeController> logger, InterfaceProductRepo productRepository)
        {
            _logger = logger;
            repository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products(string category, int productPage = 1) => View(new ProductListViewModel
        {
            Products = repository.Products
            .Where(b => category == null || b.Category == category)
            .OrderBy(b => b.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                repository.Products.Count() :
                repository.Products.Where(b =>
                    b.Category == category).Count()

            },
            CurrentCategory = category
        });
        

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
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
