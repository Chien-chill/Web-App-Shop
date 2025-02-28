using Microsoft.AspNetCore.Mvc;

namespace Project_ShoeStore_Manager.Controllers
{
    [Route("admin")]
    [Route("admin/Dashboard")]
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
