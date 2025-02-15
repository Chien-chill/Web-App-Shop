using Project_ShoeStore_Manager.Models;

namespace Project_ShoeStore_Manager.DTOs
{
    public class ReceiptDto
    {
        public string UserId { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
    }
}
