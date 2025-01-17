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
    }
}
