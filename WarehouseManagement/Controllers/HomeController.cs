using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ProductWarehouseContext context;
        public HomeController(ILogger<HomeController> logger, ProductWarehouseContext context)
        {
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string accountJson = HttpContext.Session.GetString("Account");
            var top4Product = context.Products.OrderByDescending(p=>p.Id).Take(4).Select(p=> new {p.Id,p.Name,p.QuantityInStock});
            List<Product> products = new List<Product>();
            foreach(var p in top4Product)
            {
               Product product = new Product();
                product.Id = p.Id;
                product.Name = p.Name;
                product.QuantityInStock = p.QuantityInStock;
                products.Add(product);
            }
            ViewData["top4Product"] = products;
            //// Deserialize the account object from JSON
            Account account = JsonSerializer.Deserialize<Account>(accountJson);
            if(account == null)
            {
                return View("Index","Login");
            }
            return View();
        }

    }
}
