using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.DTOs;
using Project_ShoeStore_Manager.Models;
using Project_ShoeStore_Manager.Services;
namespace Project_ShoeStore_Manager.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ShoesDbContext context;
        private readonly IWebHostEnvironment environment;
        public BrandsController(ShoesDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await context.Brands.Where(b => !b.isDeleted).ToListAsync();
            return View(brands);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandDto brandDto)
        {
            var ImageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(brandDto.BrandLogoImage.FileName);
            string ImageFullPath = Path.Combine(environment.WebRootPath, "uploads", ImageName);
            using (var stream = System.IO.File.Create(ImageFullPath))
            {
                await brandDto.BrandLogoImage.CopyToAsync(stream);
            }
            Brand brand = new Brand()
            {
                BrandName = brandDto.BrandName,
                BrandLogoImage = ImageName
            };
            await context.Brands.AddAsync(brand);
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
            var brand = await context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            brand.isDeleted = true;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
