using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MrsCleanCapstone.Data;
using MrsCleanCapstone.GenericRepository;
using MrsCleanCapstone.Models;
using MrsCleanCapstone.Models.ViewModels;

namespace MrsCleanCapstone.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private IGenericRepository<Product> _repository = null;
        public int PageSize = 4;

        public ProductsController(ILogger<ProductsController> logger, IWebHostEnvironment hostEnvironment, IGenericRepository<Product> repository)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index(string category, int productPage = 1) => View(nameof(Index), new ProductListViewModel
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
        public IActionResult Add()
        {
            return View(nameof(Add));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Add([Bind("ProductID,ProductName,Quantity,Price,Category,Description,ProductImage")] Product product, IFormFile Image)
        {

            if (ModelState.IsValid)
            {
                if (Image != null)

                {
                    if (Image.Length > 0)

                    

                    {
                        byte[] imageData = null;
                        using (var fs1 = Image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            imageData = ms1.ToArray();
                        }
                        product.ProductImage = imageData;

                    }

                    await _repository.Add(product);
                    return RedirectToAction("Index", "Products");
                }

                return View();

            }

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProductID,ProductName,Quantity,Price,Category,description,ProductImageName, ProductImage")] Product product)
        {

            if (!ProductExists(product.ProductID))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public bool ProductExists(int id)
        {
            return _repository.GetExists(e => e.ProductID == id);
        }
    }
}
