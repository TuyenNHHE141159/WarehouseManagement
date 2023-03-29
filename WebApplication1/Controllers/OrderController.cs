using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {

        // GET: OrderController
        private readonly ProductWarehouseContext context;
        DAO dao= new DAO();
        public OrderController(ProductWarehouseContext context)
        {
            this.context = context;
        }
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var customers = context.Orders.OrderBy(c => c.Id);

            var customerProcedure = dao.GetOrdersProcedure(page, pageSize);
            int totalCount = customers.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

           // customers = (IOrderedQueryable<Order>)customers.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(customerProcedure);
        }
        [HttpGet]
        public IActionResult GetProducts(string keyword)
        {
            var products = context.Products
                .Where(p => p.Name.Contains(keyword))
                .Select(p => new { label = p.Name, value = p.Id })
                .ToList();

            return Json(products);
        }

        [HttpPost]
        public IActionResult Save([FromBody] OrderModel model)
        {
            try
            {
                List<OrderDetails> orderDetails = model.OrderDetails;
                if (orderDetails.Count == 0)
                {
                    return BadRequest("Null value");
                }
                string selectedItem = model.SelectedItem;
                Order order = new Order();
                order.OrderType = selectedItem;
                order.OrderDate = DateTime.Now;
                order.CreatedBy = "tuyennh";
                context.Orders.Add(order);
                context.SaveChanges();
                foreach (var o in orderDetails)
                {
                    OrderProduct op = new OrderProduct();
                    op.ProductId = o.productId;
                    op.OrderId = order.Id;
                    op.Quantity = o.quantity.ToString();
                    op.Price = o.price;
                    context.OrderProducts.Add(op);
                    context.SaveChanges();
                }
                return Ok(orderDetails[0].productId.ToString() + " " + selectedItem);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }          
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = from o in context.Orders
                        join od in context.OrderProducts on o.Id equals od.OrderId
                        join p in context.Products on od.ProductId equals p.Id
                        where o.Id == id    
                        select new OrderDetailDTO
                        {
                            OrderID= (int)id,
                            OrderType=o.OrderType,
                            OrderDate= (DateTime)o.OrderDate,
                            OrderBy= o.CreatedBy,
                            ProductName=p.Name,
                            Quantiy=Int32.Parse( od.Quantity),
                            Price= float.Parse( od.Price)
                        };
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.OrderD = order.ToList();
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var product= context.Products.ToList();
            return View(product);
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
