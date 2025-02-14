using Microsoft.AspNetCore.Mvc;

namespace Project_ShoeStore_Manager.Controllers
{
    public class BannersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
