using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.DTOs

{
    public class SupplierDto
    {
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
