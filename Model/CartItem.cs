namespace EcommerceAPI.Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
    }
}
