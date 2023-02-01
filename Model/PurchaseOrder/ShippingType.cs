using EcommerceAPI.Models;

namespace EcommerceAPI.Model.PurchaseOrder
{
    public class ShippingType :ClassBase
    {
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
