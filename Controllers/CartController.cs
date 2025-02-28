using Microsoft.AspNetCore.Mvc;
using Project_ShoeStore_Manager.Services;
using System.Security.Claims;

namespace Project_ShoeStore_Manager.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoesDbContext context;
        public CartController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Carts = await context.Shop
            return View();
        }
    }
}
