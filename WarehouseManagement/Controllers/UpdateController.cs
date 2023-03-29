using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class UpdateController : Controller
    {
        WarehouseContext context = new();
        public IActionResult Index(int id)
        {
            int currentQuantity = 0;
            if (id > 0)
            {
                Flower currentFlower= context.Flowers.Find(id);
                if(currentFlower != null)
                {
                    currentQuantity= (int) currentFlower.Quantity;

                }
            }
            return View();
        }
    }
}
