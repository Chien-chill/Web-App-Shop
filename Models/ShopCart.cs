﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ShoeStore_Manager.Models
{
    public class ShopCart
    {
        [Key]
        public int CartId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public virtual ProductColor ProductColor { get; set; }
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public virtual ProductSize ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
