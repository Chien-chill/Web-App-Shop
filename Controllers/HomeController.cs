using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.Services;
using System.Security.Claims;

namespace Project_ShoeStore_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShoesDbContext context;
        public HomeController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await context.Products.Include(pi => pi.ProductImages).ToListAsync();
            ViewData["CartCount"] = context.ShopCart.Where(c => c.UserId == userId)?.Count() ?? 0;
            ViewData["WishlistCount"] = context.WishList.Where(w => w.UserId == userId)?.Count() ?? 0;
            return View(products);
        }
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await context.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.ProductSizes)  // Lấy danh sách size
                        .Include(p => p.ProductColors) // Lấy danh sách màu
                        .Include(p => p.ProductImages) // Lấy danh sách hình ảnh
                        .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
