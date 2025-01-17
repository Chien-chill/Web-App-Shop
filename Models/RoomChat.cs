using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class RoomChat
    {
        [Key]
        public int RoomId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
