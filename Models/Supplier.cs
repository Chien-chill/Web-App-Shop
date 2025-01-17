using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
