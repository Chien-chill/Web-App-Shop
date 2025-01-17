using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.Models
{
    public class Banner
    {
        [Key]
        public int BannerId { get; set; }
        [Required]
        [MaxLength(20)]
        public string BannerTitle { get; set; }
        [Required]
        public string BannerImage { get; set; }
    }
}
