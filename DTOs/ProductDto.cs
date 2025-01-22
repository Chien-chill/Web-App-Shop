using Project_ShoeStore_Manager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.DTOs
{
    public class ProductDto
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        public string? ProductDescription { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public decimal ProfitMargin { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreateAt { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
        public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
    }
}
