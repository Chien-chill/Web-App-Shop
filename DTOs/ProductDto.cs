using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.DTOs
{
    public class ProductDto
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
        public string? ProductDescription { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public decimal ProfitMargin { get; set; }
        [Required]
        public string ProductSizes { get; set; }
        [Required]
        public string ProductColors { get; set; }
        public List<IFormFile> ProductImages { get; set; }
        public List<string> ProductImagesUrl { get; set; } = new List<string>();
    }
}
