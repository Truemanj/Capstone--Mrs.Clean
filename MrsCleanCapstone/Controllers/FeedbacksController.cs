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
    public class FeedbacksController : Controller
    {
        private readonly ILogger<FeedbacksController> _logger;
        private IGenericRepository<Feedback> _feedbackRepository = null;

        public FeedbacksController(ILogger<FeedbacksController> logger, IGenericRepository<Feedback> feedbackRepository)
        {
            _logger = logger;
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IActionResult> Create (Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.DateSubmitted = DateTime.Now;
                await _feedbackRepository.Add(feedback);
                TempData["SUCCESS"] = "SUCCESS";
                return RedirectToAction("Index", "Home");
            }
            TempData["ERROR"] = "ERROR";
            return RedirectToAction("Index", "Home");
        }
    }
}
