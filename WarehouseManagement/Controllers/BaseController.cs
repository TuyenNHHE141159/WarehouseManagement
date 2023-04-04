using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Controllers
{
    public class BaseController : Controller
    {
        public bool checkAuthoration()
        {
            string username = HttpContext.Session.GetString("username");
            int role_id = (int)HttpContext.Session.GetInt32("role_id");
            if (role_id != 0 && username!=null)
            {
                return true;
            }
            return false;
        }
    }
}
