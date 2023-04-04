using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ProductWarehouseContext context;
        DAO dao = new DAO();
        public CustomerController(ProductWarehouseContext context) {
        this.context = context;
        }
        public IActionResult Index()
        {

            return View(context.Customers.ToList());
        }
        public IActionResult Add()
        {

            return View();
        }
        public IActionResult List(int customerId)
        {
            var list = from c in context.Customers
                       join o in context.Orders on c.Id equals o.CustomerId 
                       where o.CustomerId == customerId  
                       select new Order
                       {
                           Id = o.Id,
                           CustomerId = o.CustomerId,
                           OrderDate = o.OrderDate,
                           OrderType = o.OrderType,
                           CreatedBy = o.CreatedBy,
                           Note = o.Note,
                           Status = o.Status

                       };
            List<Order> orders = new List<Order>();


            foreach (var item in list)
            {
                Order o = new Order();
                o.Id = item.Id;
                o.CustomerId = item.CustomerId;
                o.OrderDate = item.OrderDate;
                o.OrderType = item.OrderType;
                o.CreatedBy = item.CreatedBy;
                o.Note = item.Note;
                o.Status = item.Status;   
                orders.Add(o);
            }
            Customer customer = context.Customers.Find(customerId);
           ViewBag.CustomerName= customer;
            return View(orders);
        }
        public ActionResult DetailsOrder(int id)
        {
            var order = dao.GetOrderDetailsByID(id);
            if (order.Count() > 0)
            {
                ViewBag.OrderId = id;
                ViewBag.OrderType = order[0].OrderType;
                ViewBag.OrderDate = order[0].OrderDate;
                ViewBag.OrderBy = order[0].OrderBy;
            }
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
