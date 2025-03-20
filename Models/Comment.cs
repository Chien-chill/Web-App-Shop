using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        public string? Content { get; set; }
        public string? Image { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public string Star { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
