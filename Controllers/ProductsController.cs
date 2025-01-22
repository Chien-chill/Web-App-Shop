using Microsoft.AspNetCore.Mvc;
using Project_ShoeStore_Manager.Services;

namespace Project_ShoeStore_Manager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoesDbContext context;
        public ProductsController(ShoesDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {

        }
    }
}
