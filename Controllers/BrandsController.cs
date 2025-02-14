using Microsoft.AspNetCore.Mvc;

namespace Project_ShoeStore_Manager.Controllers
{
    public class BrandsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
