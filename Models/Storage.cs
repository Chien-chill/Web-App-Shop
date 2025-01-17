using System.ComponentModel.DataAnnotations;

namespace Project_ShoeStore_Manager.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string SizeId { get; set; }
        public virtual ProductSize Size { get; set; }
        public string ColorId { get; set; }
        public virtual ProductColor Color { get; set; }
        public int SellAbleQuantity { get; set; }
        public int StockQuantity { get; set; }
    }
}
