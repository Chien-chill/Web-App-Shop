using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
