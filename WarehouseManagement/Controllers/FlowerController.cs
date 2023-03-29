using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class FlowerController : Controller
    {
        WarehouseContext context = new();
        public IActionResult Index()
        {
            
            return View(context.Flowers.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flower = context.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }
            return View(flower);
        }
        public ActionResult AddOrSub(int? id, string type)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flower = context.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }
            if (type != null)
            {
                ViewBag.Type = type;
            }
            return View(flower);
        }
        [HttpPost]
        public ActionResult AddOrSub(int? id, int quantityChange, string type)
        {
            if (id == null || quantityChange == 0)
            {
                return NotFound();
            }
            else
            {
                if (type.Equals("add"))
                {
                    Flower flower = context.Flowers.Find(id);
                    flower.Quantity = flower.Quantity + quantityChange;
                    ViewBag.Type = type;
                    context.SaveChanges();
                }
                else
                {
                    Flower flower = context.Flowers.Find(id);
                    flower.Quantity = flower.Quantity - quantityChange;
                    ViewBag.Type = type;
                    if (flower.Quantity < 0)
                    {
                        ViewBag.MessageWrong = "Not enough";
                        return View(flower);
                    }
                    else
                    {
                    context.SaveChanges();
                    }
                    
                }

            }
            return RedirectToAction("Index", "Flower");
        }

        [HttpPost]
        public ActionResult Edit(int? id, Flower flower)
        {
            if (id != flower.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(flower);
                context.SaveChanges();
                RedirectToAction("Index", "Home");
            }
            return View(flower);
        }
    }
}
