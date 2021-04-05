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
        private readonly ILogger<HomeController> _logger;

        private readonly IGenericRepository<Product> _repository;
        public int PageSize = 4;

        public HomeController(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(nameof(Index));
        }

        public IActionResult Products(string category, int productPage = 1) => View(nameof(Products),new ProductListViewModel
        {
            Products = _repository.Get()
            .Where(b => category == null || b.Category == category)
            .OrderBy(b => b.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                _repository.Get().Count() :
                _repository.Get().Where(b =>
                    b.Category == category).Count()

            },
            CurrentCategory = category
        });
        

        [Authorize]
        public IActionResult Privacy()
        {
            return View(nameof(Privacy));
        }

        public IActionResult Checklist()
        {
            return View(nameof(Checklist));
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
