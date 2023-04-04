using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class ProductController : Controller

    {
        private readonly ProductWarehouseContext context;
        public ProductController(ProductWarehouseContext context)
        {
            this.context = context;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var list = from p in context.Products
                       join c in context.Categories on p.CategoryId equals c.Id
                       select new
                       {
                           Id = p.Id,
                           Name = p.Name,
                           QuantityInStock = p.QuantityInStock,
                           CategoryId = c.Id,
                           CategoryName = c.Name
                       };
          List<Product> l= new List<Product>();
            foreach(var item in list)
            {
                Product product = new Product();
                product.Id = item.Id;
                product.Name = item.Name;
                product.QuantityInStock = item.QuantityInStock;
               Category category = new Category();
                category.Id = item.CategoryId;
                category.Name = item.CategoryName;
                product.Category = category;
                l.Add(product);
            }
            return View(l.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
