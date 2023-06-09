﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: ExpenseController
        public ActionResult List()
        {
            return View();
        }

        // GET: ExpenseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExpenseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExpenseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExpenseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExpenseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}
