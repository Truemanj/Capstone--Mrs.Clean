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
        [Route("/{controller}/")]
        public async Task<IActionResult> Deals()
        {
            var deals = (await _repository.GetAll()).ToList();
            return View(nameof(Deals), deals);
        }

    }
}
