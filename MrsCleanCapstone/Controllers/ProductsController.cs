using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

        // GET: Products
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

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository
                .FirstOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/AddProduct
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Add([Bind("ProductID,ProductName,Quantity,Price,Category,description,ProductImageName, ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductImage != null)
                {
                    //Save image to wwwroot
                    string wwwrootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(product.ProductImage.FileName);
                    string extension = Path.GetExtension(product.ProductImage.FileName);

                    product.ProductImageName = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(wwwrootPath + "/Images/", filename);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.ProductImage.CopyToAsync(fileStream);
                    }
                }
                await _repository.Add(product);
                return RedirectToAction("Index", "Products");
            }

            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    if (product.ProductImage != null)
                    {
                        string wwwrootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(product.ProductImage.FileName);
                        string extension = Path.GetExtension(product.ProductImage.FileName);

                        if (product.ProductImageName != null)
                        {
                            if (System.IO.File.Exists(Path.Combine(wwwrootPath + "/Images/", product.ProductImageName)))
                            {
                                System.IO.File.Delete(Path.Combine(wwwrootPath + "/Images/", product.ProductImageName));
                            }
                        }

                        product.ProductImageName = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;

                        string path = Path.Combine(wwwrootPath + "/Images/", filename);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product.ProductImage.CopyToAsync(fileStream);
                        }
                    }

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
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repository.FirstOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _repository.GetById(id);
            await _repository.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        public bool ProductExists(int id)
        {
            return _repository.GetExists(e => e.ProductID == id);
        }
    }
}
