﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page == null || page < 0) ? 1 : page.Value;
            var products = context.Products.Include(pi => pi.ProductImages).ToPagedList(pageNumber, pageSize);
            ViewData["BrandId"] = new SelectList(context.Brands, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "CategoryName");
            return View(products);
        }

    }
}
