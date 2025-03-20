using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.Models;
using Project_ShoeStore_Manager.Services;
using System.Security.Claims;

namespace Project_ShoeStore_Manager.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ShoesDbContext context;
        public WishlistController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishList = await context.Favorite.Where(f => f.UserId == userId)
                                           .Include(f => f.Product)
                                           .ThenInclude(f => f.ProductImages).ToListAsync();

            return View(wishList);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/LoginRegister", new { area = "Identity" });
            }
            Favorite favorite = new Favorite()
            {
                UserId = userId,
                ProductId = ProductId
            };
            await context.Favorite.AddAsync(favorite);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var favorite = await context.Favorite.FindAsync(id);
            context.Favorite.Remove(favorite);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
