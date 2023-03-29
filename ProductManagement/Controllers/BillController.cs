using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagement.Controllers
{
    public class BillController : Controller
    {
        ProductWarehouseContext context = new();
        // GET: BillControllers
        public ActionResult Index()
        {
            var listBill = from b in context.Bills
                           join c in context.Customers on b.CustomerId equals c.CustomerId
                           join t in context.BillTypes on b.BillTypeNavigation.Id equals t.Id
                           select new
                           {
                               BillId = b.BillId,
                               CustomerName= c.CustomerName,
                               BillType = b.BillType,
                               PaidStatus= b.PaidStatus,
                               CreatedDate= b.CreatedDate
                           };
               
            ViewBag.Bill = listBill;
            return View(context.Bills.ToList());
        }

        // GET: BillController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null) {
                return NotFound();
                    }
            var bill = context.Bills.FirstOrDefault(m=>m.BillId==id);
            if(bill==null) { return NotFound(); }   
            return View(bill);
        }

        // GET: BillController/Create
        public ActionResult Create()
        {
            var billTypes = context.BillTypes
        .Select(bt => new SelectListItem
        {
            Value = bt.Id.ToString(),
            Text = bt.Type.ToString(),
        })
        .ToList();

            ViewBag.BillType = new SelectList(billTypes, "Value", "Text");

            var customers = context.Customers
       .Select(bt => new SelectListItem
       {
           Value = bt.CustomerId.ToString(),
           Text = bt.CustomerName.ToString(),
       })
       .ToList();

            ViewBag.CustomerId = new SelectList(customers, "Value", "Text");
            return View();
        }

        // POST: BillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                context.Bills.Add(bill);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }

        // GET: BillController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillController/Edit/5
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

        // GET: BillController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillController/Delete/5
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
