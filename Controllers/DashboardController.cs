using Microsoft.AspNetCore.Mvc;

namespace Project_ShoeStore_Manager.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
