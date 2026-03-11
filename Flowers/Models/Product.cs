namespace Flowers.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}

