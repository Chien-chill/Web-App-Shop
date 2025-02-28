using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.Services;

namespace Project_ShoeStore_Manager.Controllers
{
    public class ReceiptsController : Controller
    {
        public readonly ShoesDbContext context;
        public ReceiptsController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var receipt = await context.Receipts.ToListAsync();
            return View(receipt);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }

    }
}
