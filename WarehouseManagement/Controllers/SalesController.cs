using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WarehouseManagement.Models;
using System.Text.Json;

namespace WarehouseManagement.Controllers
{
   
    public class SalesController : Controller
    {
        private readonly ProductWarehouseContext  context= new ProductWarehouseContext();
        private readonly DAO  dao= new DAO();
        const int roleAdmin= 1;
        const int roleStaff = 2;
        const int nullUser = 1;
        const int noAuthorization = 2;
        const int hadAuthorization = 3;

        // GET: SalesController
        public bool verifyAccount(int roleID)
        {
            string accountJson = HttpContext.Session.GetString("Account");
            //// Deserialize the account object from JSON
            if (accountJson == null)
            {
                 RedirectToAction("Index", "Login");
            }
            Account account = JsonSerializer.Deserialize<Account>(accountJson);
            var count = context.AccountRoles.Count(m => m.AccountId == account.AccountId && m.RoleId == roleID);
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult Index()
        {
            if (verifyAccount(roleStaff) == false)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                return RedirectToAction("Index", "Home");
            }
            return View(context.Orders.OrderBy(o=>o.OrderDate).ToList());      
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            if (verifyAccount(roleStaff) == false)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                return RedirectToAction("Index", "Home");
            }
            var order = dao.GetOrderDetailsByID(id);
            var customerName = from c in context.Customers
                               join o in context.Orders on c.Id equals o.CustomerId
                               where o.Id == id
                               select new
                               {
                                   c.Name
                               };
            string name = customerName.FirstOrDefault()?.Name;
            if (order.Count() > 0)
            {
                ViewBag.OrderId = id;
                ViewBag.OrderType = order[0].OrderType;
                ViewBag.OrderDate = order[0].OrderDate;
                ViewBag.OrderBy = order[0].OrderBy;
                ViewBag.customerName= name;
            }   
            if (order == null)
            {
                return NotFound();
            }
            //ViewBag.OrderD = order.ToList();
            return View(order);
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            if (verifyAccount(roleAdmin) == false)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
               return RedirectToAction("Index", "Home");
            }
            return View();
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

        [HttpGet]
        public IActionResult GetCustomers(string keyword)
        {
            var customers = context.Customers
                .Where(p => p.Name.Contains(keyword))
                .Select(p => new { label = p.Name, value = p.Id })
                .ToList();

            return Json(customers);
        }

        [HttpPost]
        public IActionResult Save([FromBody] OrderModel model)
        {
            try
            {
                string accountJson = HttpContext.Session.GetString("Account");
                if (accountJson == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                //// Deserialize the account object from JSON
                Account account = JsonSerializer.Deserialize<Account>(accountJson);
                // int role_id = (int)HttpContext.Session.GetInt32("role_id");
                var count = context.AccountRoles.Count(m => m.AccountId == account.AccountId && (m.RoleId == 1));
                if (count == 0)
                {
                    TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                    return RedirectToAction("Index", "Home");
                }
                List<OrderDetails> orderDetails = model.OrderDetails;
                if (orderDetails.Count == 0)
                {
                    return BadRequest("Null value");
                }

                if (model.SelectedItem.Equals("export"))
                {
                    foreach(var item in orderDetails)
                    {
                        int exportQ = item.quantity;
                        int productId = item.productId;
                        Product product= context.Products.Find(item.productId);
                        if (exportQ > product.QuantityInStock)
                        {
                            return View("Index","Sales");
                        }
                    }
                }
                string selectedItem = model.SelectedItem;
                Order order = new Order();
                order.OrderType = selectedItem;
                order.OrderDate = DateTime.Now;
                order.CreatedBy = account.Username;
                order.CustomerId = model.CustomerId;
                context.Orders.Add(order);
                context.SaveChanges();
                foreach (var o in orderDetails)
                {
                    OrderProduct op = new OrderProduct();
                    op.ProductId = o.productId;
                    op.OrderId = order.Id;
                    op.Quantity = o.quantity;

                    int quantity = o.quantity;
                    int productId = o.productId;
                    Product product = context.Products.Find(o.productId);
                    if (selectedItem.Equals("import"))
                    {
                        product.QuantityInStock += quantity;
                    }
                    else
                    {
                        product.QuantityInStock -= quantity;
                    }
                    op.Price = o.price;
                    context.OrderProducts.Add(op);
                    context.SaveChanges();
                }
                return Ok(orderDetails[0].productId.ToString() + " " + selectedItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST: SalesController/Create
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

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id, string type)
        {
            if (verifyAccount(roleAdmin) == false)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                return RedirectToAction("Index", "Home");
            }
            var customerName = from c in context.Customers
                               join o in context.Orders on c.Id equals o.CustomerId
                               where o.Id == id
                               select new
                               {
                                   c.Name
                               };
            string name = customerName.FirstOrDefault()?.Name;
            ViewBag.customerName = name;
            List<OrderDetails> list = dao.GetOrderDetails(id);
            OrderModel order = new OrderModel();
            order.OrderId = id;
            order.OrderDetails = list;
            order.SelectedItem = type;
            return View(order);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([FromBody] OrderModel model)
        {
            try
            {
                if (verifyAccount(roleAdmin) == false)
                {
                    TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                    return RedirectToAction("Index", "Home");
                }
                List<OrderDetails> orderDetails = model.OrderDetails;
                if (orderDetails.Count == 0)
                {
                    return BadRequest("Null value");
                }
                string selectedItem = model.SelectedItem;
               

                //xử lý list order cũ trước khi sửa
                List<OrderProduct> productList = dao.GetOrderDetailByOrderId(model.OrderId);
                if(productList.Count > 0)
                {
                    foreach (var product in productList)
                    {
                        int oldQuantity= (int)product.Quantity;
                        int oldProductId= (int) product.ProductId;
                        Product currentProduct = context.Products.Find(oldProductId);
                        if (currentProduct != null)
                        {
                            if (selectedItem.Equals("import"))
                            {
                                currentProduct.QuantityInStock -= oldQuantity;
                            }
                            else
                            {
                                currentProduct.QuantityInStock += oldQuantity;
                            }
                        }
                    }
                }

                bool delete = dao.DeleteAllOrder(model.OrderId);

                //xử lý list order mới sau khi sửa
                foreach (var o in orderDetails)
                {
                    OrderProduct op = new OrderProduct();
                    op.ProductId = o.productId;
                    op.OrderId = model.OrderId;
                    op.Quantity = o.quantity;
                     int quantity = o.quantity;
                    int productId = o.productId;
                    Product product = context.Products.Find(o.productId);
                    if (selectedItem.Equals("import"))
                    {
                        product.QuantityInStock += quantity;
                    }
                    else
                    {
                        product.QuantityInStock -= quantity;
                    }
                    op.Price = o.price;
                    context.OrderProducts.Add(op);
                    context.SaveChanges();
                }
                return Ok(orderDetails[0].productId.ToString() + " " + selectedItem);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        // GET: SalesController/Delete/5
        public ActionResult DeleteView(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (verifyAccount(roleAdmin) == false)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này";
                RedirectToAction("Index", "Home");
            }
            string type = context.Orders.Find(id).OrderType;       
            List<OrderProduct> productList = dao.GetOrderDetailByOrderId(id);
            if (productList.Count > 0)
            {
                foreach (var product in productList)
                {
                    int oldQuantity = (int)product.Quantity;
                    int oldProductId = (int)product.ProductId;
                    Product currentProduct = context.Products.Find(oldProductId);
                    if (currentProduct != null)
                    {
                        if (type.Equals("import"))
                        {
                            currentProduct.QuantityInStock -= oldQuantity;
                        }
                        else
                        {
                            currentProduct.QuantityInStock += oldQuantity;
                        }
                    }
                }
            }
            bool delete = dao.DeleteAllOrder(id);
            Order deleteOrder= context.Orders.Find(id);
            context.Orders.Remove(deleteOrder);
            context.SaveChanges();
            return RedirectToAction("Index","Sales");
            
        }
    }
}
