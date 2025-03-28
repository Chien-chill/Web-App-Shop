using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.DTOs;
using Project_ShoeStore_Manager.Services;
using X.PagedList.Extensions;
namespace Project_ShoeStore_Manager.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShoesDbContext context;
        public ShopController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int? page, FilterProduct filter)
        {
            int pageSize = 3;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;

            var query = context.Products.AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == filter.BrandId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.SellingPrice >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.SellingPrice <= filter.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(p => p.ProductName.Contains(filter.SearchString));
            }

            switch (filter.SortBy?.ToLower())
            {
                case "price_asc":
                    query = query.OrderBy(p => p.SellingPrice);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.SellingPrice);
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.ProductName);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreateAt);
                    break;
            }

            query = query.Include(pi => pi.ProductImages);

            var products = query.ToPagedList(pageNumber, pageSize);

            ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName", filter.BrandId);
            ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName", filter.CategoryId);

            // Lưu filter vào ViewData để giữ lại giá trị đã chọn
            ViewData["Filter"] = filter;

            return View(products);
        }
    }
}
