using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.DTOs;
using Project_ShoeStore_Manager.Models;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/LoginRegister", new { area = "Identity" });

            }
            var cartList = await context.ShopCart.Where(c => c.UserId.Equals(userId))
                                                 .Include(c => c.Product)
                                                  .ThenInclude(c => c.ProductImages)
                                                 .Include(c => c.ProductSize)
                                                 .Include(c => c.ProductColor)
                                                 .ToListAsync();
            return View(cartList);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CartDto cartDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToPage("/Account/LoginRegister", new { area = "Identity" });
            }
            bool exist_CartItem = await context.ShopCart.AnyAsync(c => c.UserId == userId
                                                                  && c.ProductId == cartDto.ProductId
                                                                  && c.SizeId == cartDto.SizeId
                                                                  && c.ColorId == cartDto.ColorId);
            if (!exist_CartItem)
            {
                var productPrice = context.Products.Where(p => p.ProductId == cartDto.ProductId).Select(p => p.SellingPrice).FirstOrDefault();
                cartDto.TotalAmount = productPrice * cartDto.Quantity;
                ShopCart cart = new ShopCart()
                {
                    UserId = userId,
                    ProductId = cartDto.ProductId,
                    SizeId = cartDto.SizeId,
                    ColorId = cartDto.ColorId,
                    Quantity = cartDto.Quantity,
                    TotalAmount = cartDto.TotalAmount
                };
                await context.ShopCart.AddAsync(cart);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return Content("Đã từng thêm");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Cartid)
        {
            if (Cartid == null)
            {
                return NotFound();
            }
            var cartItem = await context.ShopCart.FindAsync(Cartid);
            if (cartItem == null)
            {
                return NotFound();
            }
            context.ShopCart.Remove(cartItem);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
