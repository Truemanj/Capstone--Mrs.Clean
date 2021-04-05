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
        private IGenericRepository<Product> _repository;

        public NavigationMenuViewComponent(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];
            return View(_repository.Get().Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
