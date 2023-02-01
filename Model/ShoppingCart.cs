namespace EcommerceAPI.Model
{
    public class ShoppingCart
    {
        public ShoppingCart() { }

        public ShoppingCart(string id)
        {
            id = id;
        }
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId   { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
