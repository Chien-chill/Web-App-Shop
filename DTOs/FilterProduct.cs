namespace Project_ShoeStore_Manager.DTOs
{
    public class FilterProduct
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public string? SearchString { get; set; }
    }
}
