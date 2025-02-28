using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.Services;

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
            var products = await context.Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.SellingPrice,
                MainImage = "/uploads/" + p.ProductImages.FirstOrDefault(image => image.IsMainImage).ImageFileName
            }).ToListAsync();
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
