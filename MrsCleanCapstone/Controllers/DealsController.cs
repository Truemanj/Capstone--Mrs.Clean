using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers
{
    public class DealsController : Controller
    {

        private readonly ILogger<DealsController> _logger;
        private IGenericRepository<Deal> _repository = null;

        public DealsController(ILogger<DealsController> logger, IGenericRepository<Deal> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        // GET: DealsController
        public ActionResult Index()
        {
            var deals = _repository.Get().ToList();
            return View(deals);
        }

        // GET: DealsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DealsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DealsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DealsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DealsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DealsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DealsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
