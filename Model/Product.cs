using EcommerceAPI.Models;

namespace EcommerceAPI.Model
{
    public class Product : ClassBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { set; get; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
