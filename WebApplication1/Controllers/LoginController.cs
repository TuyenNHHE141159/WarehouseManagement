using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        DAO dao= new DAO();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return View("Index","Login");
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Account account = dao.Login(username, password);

            if (account == null)
            {
                ViewBag.Message = "Login failed";
                return View("Index", "Login");
            }
            else
            {
                //HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Order");
            }

        }
    }
}
