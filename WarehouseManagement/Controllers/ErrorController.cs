using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
