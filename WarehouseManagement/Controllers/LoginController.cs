using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    public class LoginController : Controller
    {
        WarehouseContext context = new WarehouseContext();
     DAO dao= new DAO();
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Account account = dao.Login(username, password);

            if (account == null)
            {
                ViewBag.Message = "Login failed";
                return View("Index","Login");
            }
            else
            {
                return RedirectToAction("Index", "Flower");
            }
             
        }
    }
}
