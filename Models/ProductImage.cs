using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageFileName { get; set; }
        public string ProductId { get; set; }
        public bool IsMainImage { get; set; } = false;
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
