using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;

namespace MrsCleanCapstone.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IGenericRepository<Product> repository;

        public NavigationMenuViewComponent(IGenericRepository<Product> productRepo)
        {
            repository = productRepo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData.Values["category"];
            return View(repository.Get().Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
