using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.DTOs;
using Project_ShoeStore_Manager.Models;
using Project_ShoeStore_Manager.Services;

namespace Project_ShoeStore_Manager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoesDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(ShoesDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await context.Products
                         .Include(p => p.Brand)
                         .Include(p => p.Category)
                         .Where(p => !p.IsDeleted)
                         .ToListAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName");
                ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName");
                return View(productDto);
            }
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    Product product = new Product();
                    {
                        product.ProductName = productDto.ProductName;
                        product.BrandId = productDto.BrandId;
                        product.CategoryId = productDto.CategoryId;
                        product.ProductDescription = productDto.ProductDescription;
                        product.PurchasePrice = productDto.PurchasePrice;
                        product.ProfitMargin = productDto.ProfitMargin;
                    }

                    product.ProductSizes = productDto.ProductSizes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(size => new ProductSize()
                                                          {
                                                              SizeName = size.Trim()
                                                          }).ToList();
                    product.ProductColors = productDto.ProductColors.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(color => new ProductColor()
                                                            {
                                                                ColorName = color.Trim()
                                                            }).ToList();


                    foreach (var image in productDto.ProductImages)
                    {
                        if (image.Length > 0)
                        {
                            var ImageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(image.FileName);
                            string ImageFullPath = Path.Combine(environment.WebRootPath, "uploads", ImageName);
                            using (var stream = System.IO.File.Create(ImageFullPath))
                            {
                                await image.CopyToAsync(stream);
                            }
                            product.ProductImages.Add(new ProductImage()
                            {
                                ImageFileName = ImageName,
                                IsMainImage = (product.ProductImages.Count == 0)
                            });
                        }
                    }
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Content("Lỗi Thêm sản phẩm !");
                }
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await context.Products
                        .Include(p => p.ProductSizes)  // Lấy danh sách size
                        .Include(p => p.ProductColors) // Lấy danh sách màu
                        .Include(p => p.ProductImages) // Lấy danh sách hình ảnh
                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDto productDto = new ProductDto();
            {
                productDto.ProductName = product.ProductName;
                productDto.ProfitMargin = product.ProfitMargin;
                productDto.PurchasePrice = product.PurchasePrice;
                productDto.ProductDescription = product.ProductDescription;
                productDto.BrandId = product.BrandId;
                productDto.CategoryId = product.CategoryId;
                productDto.ProductColors = string.Join(",", product.ProductColors.Select(pc => pc.ColorName));
                productDto.ProductSizes = string.Join(",", product.ProductSizes.Select(ps => ps.SizeName));
                foreach (var pi in product.ProductImages)
                {
                    string ImagePath = "/uploads/" + pi.ImageFileName;
                    productDto.ProductImagesUrl.Add(ImagePath);
                }
            }
            ViewData["ProductId"] = product.ProductId.ToString();
            ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName", productDto.BrandId);
            ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName", productDto.CategoryId);
            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductDto productDto)
        {
            var product = await context.Products
                        .Include(p => p.ProductSizes)  // Lấy danh sách size
                        .Include(p => p.ProductColors) // Lấy danh sách màu
                        .Include(p => p.ProductImages) // Lấy danh sách hình ảnh
                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.ProductId.ToString();
                ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName", productDto.BrandId);
                ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName", productDto.CategoryId);
                return View(productDto);
            }
            product.ProductName = productDto.ProductName;
            product.BrandId = productDto.BrandId;
            product.CategoryId = productDto.CategoryId;
            product.PurchasePrice = productDto.PurchasePrice;
            product.ProfitMargin = productDto.ProfitMargin;

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await context.Products
                           .Include(p => p.ProductImages)
                           .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            foreach (var pi in product.ProductImages)
            {
                string ImagePath = Path.Combine(environment.WebRootPath, "uploads", pi.ImageFileName);
                System.IO.File.Delete(ImagePath);
            }
            product.IsDeleted = true;
            context.ProductImages.RemoveRange(product.ProductImages);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}


//context.ProductSizes.RemoveRange(product.ProductSizes);
//context.ProductColors.RemoveRange(product.ProductColors);
//context.ProductImages.RemoveRange(product.ProductImages);
//product.ProductSizes = productDto.ProductSizes.Split(',', StringSplitOptions.RemoveEmptyEntries)
//                                      .Select(size => new ProductSize()
//                                      {
//                                          SizeName = size.Trim()
//                                      }).ToList();
//product.ProductColors = productDto.ProductColors.Split(',', StringSplitOptions.RemoveEmptyEntries)
//                                        .Select(color => new ProductColor()
//                                        {
//                                            ColorName = color.Trim()
//                                        }).ToList();
//foreach (var image in productDto.ProductImages)
//{
//    if (image.Length > 0)
//    {
//        var ImageName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(image.FileName);
//        string ImageFullPath = Path.Combine(environment.WebRootPath, "uploads", ImageName);
//        using (var stream = System.IO.File.Create(ImageFullPath))
//        {
//            await image.CopyToAsync(stream);
//        }
//        product.ProductImages.Add(new ProductImage()
//        {
//            ImageFileName = ImageName,
//            IsMainImage = (product.ProductImages.Count == 0)
//        });
//    }
//}