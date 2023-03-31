using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ReportController : Controller
    {
        DAO dao = new DAO();
        public IActionResult Report(string type)
        {
            if (type == null)
            {
                type = "import";
            }
            ViewBag.Type = type;    
            return View(dao.ReportProductQuantity(type));
        }
    }
}
