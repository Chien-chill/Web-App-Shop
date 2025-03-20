using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class Message
    {
        [Key]
        public int MesageId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public RoomChat RoomChat { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
