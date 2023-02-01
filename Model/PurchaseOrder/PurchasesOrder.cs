
using EcommerceAPI.Models;

namespace EcommerceAPI.Model.PurchaseOrder
{
    public class PurchasesOrder : ClassBase
    {
        public PurchasesOrder()
        {

        }

        public PurchasesOrder(string buyerEmail, Address shippingAddress, ShippingType shippingType, IReadOnlyList<OrderItem> orderItems, decimal subtotal, string paymentAttemptId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            ShippingType = shippingType;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentAttemptId = paymentAttemptId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset PurchaseOrder { get; set; } = DateTimeOffset.Now;
        public Address ShippingAddress { get; set; }
        public ShippingType ShippingType { get; set; }
        public IReadOnlyList <OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public StatusOrder Status { get; set; } = StatusOrder.Pending;

        public string PaymentAttemptId { get; set; }
        public decimal GetTotal()
        {
            return Subtotal + ShippingType.Price;
        }
    }
}
