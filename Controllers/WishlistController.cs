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
            if (userId == null)
            {
                return RedirectToPage("/Account/LoginRegister", new { area = "Identity" });
            }
            var wishList = await context.WishList.Where(f => f.UserId == userId)
                                           .Include(f => f.Product)
                                           .ThenInclude(f => f.ProductImages).ToListAsync();

            return View(wishList);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/LoginRegister", new { area = "Identity" });
            }
            if (!await context.WishList.AnyAsync(w => w.UserId == userId && w.ProductId == id))
            {
                Wishlist wishlist = new Wishlist()
                {
                    ProductId = id,
                    UserId = userId
                };
                await context.WishList.AddAsync(wishlist);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return Content("Đã có r");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var wishlist = await context.WishList.FindAsync(id);
            context.WishList.Remove(wishlist);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
