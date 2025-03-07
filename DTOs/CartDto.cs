namespace Project_ShoeStore_Manager.DTOs
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
