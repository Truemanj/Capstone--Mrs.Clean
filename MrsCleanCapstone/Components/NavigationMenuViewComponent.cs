using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrsCleanCapstone.Models;

namespace MrsCleanCapstone.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private InterfaceProductRepo repository;

        public NavigationMenuViewComponent(InterfaceProductRepo productRepo)
        {
            repository = productRepo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];
            return View(repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
