using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{

    public class LoginController : Controller
    {
        DAO dao = new DAO();
        private readonly ProductWarehouseContext context;
        public LoginController(ProductWarehouseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Account");

            return View("Index", "Login");
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Account account = dao.Login(username, password);
            // List<AccountRole> roles = account.AccountRoles.ToList();
            if (account == null)
            {
                ViewBag.Message = "Login failed";
                return View("Index", "Login");
            }
            else
            {
                // Serialize the account object to JSON
                string accountJson = JsonSerializer.Serialize(account);

                // Store the serialized account in session
                HttpContext.Session.SetString("Account", accountJson);
                HttpContext.Session.SetString("Username", account.Username);
                //string accountJson = HttpContext.Session.GetString("Account");

                //// Deserialize the account object from JSON
                //Account account = JsonSerializer.Deserialize<Account>(accountJson);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
