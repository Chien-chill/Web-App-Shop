using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        [MaxLength(20)]
        public string BrandName { get; set; }
        public string BrandLogoImage { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
