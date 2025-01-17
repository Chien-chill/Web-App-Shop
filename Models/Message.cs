using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public RoomChat RoomChat { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
