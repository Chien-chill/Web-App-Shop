using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class ReceiptDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReceiptId { get; set; }
        [ForeignKey("ReceiptId")]
        public virtual Receipt Receipt { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual ProductSize Size { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public virtual ProductColor Color { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
