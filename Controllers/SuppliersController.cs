using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.DTOs;
using Project_ShoeStore_Manager.Models;
using Project_ShoeStore_Manager.Services;

namespace Project_ShoeStore_Manager.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ShoesDbContext context;
        public SuppliersController(ShoesDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var supplier = await context.Supplier.Where(su => !su.isDeleted).ToListAsync();
            return View(supplier);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierDto);
            }
            Supplier supplier = new Supplier()
            {
                SupplierName = supplierDto.SupplierName,
                Address = supplierDto.Address,
                PhoneNumber = supplierDto.PhoneNumber,
                Email = supplierDto.Email,
            };
            await context.Supplier.AddAsync(supplier);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            SupplierDto supplierDto = new SupplierDto()
            {
                SupplierName = supplier.SupplierName,
                Address = supplier.Address,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email
            };
            ViewData["SupplierId"] = supplier.SupplierId.ToString();
            return View(supplierDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SupplierDto supplierDto)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(supplierDto);
            }
            supplier.SupplierName = supplierDto.SupplierName;
            supplier.Address = supplierDto.Address;
            supplier.PhoneNumber = supplierDto.PhoneNumber;
            supplier.Email = supplierDto.Email;
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
            var supplier = await context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            supplier.isDeleted = true;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
