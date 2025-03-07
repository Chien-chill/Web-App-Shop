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
                return Redirect("/Identity/Account/Login");
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
                return Redirect("/Identity/Account/Login");
            }
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
    }
}
