using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_ShoeStore_Manager.Models;
namespace Project_ShoeStore_Manager.Services
{
    public class ShoesDbContext : IdentityDbContext
    {
        public ShoesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public DbSet<RoomChat> RoomChats { get; set; }
        public DbSet<Storage> Storages { get; set; }

    }
}
