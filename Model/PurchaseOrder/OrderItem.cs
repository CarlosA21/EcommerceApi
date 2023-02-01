using EcommerceAPI.Models;

namespace EcommerceAPI.Model.PurchaseOrder
{
    public class OrderItem : ClassBase
    {
        public OrderItem() 
        {
        }

        public OrderItem(OrderedProductItem orderedItem, decimal price, int amount)
        {
            OrderedItem = orderedItem;
            Price = price;
            Amount = amount;
        }

        public OrderedProductItem OrderedItem { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
