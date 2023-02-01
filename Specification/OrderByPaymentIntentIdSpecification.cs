using Core.Specification;
using EcommerceAPI.Model.PurchaseOrder;

namespace EcommerceAPI.Specification
{
    public class OrderByPaymentIntentIdSpecification :BaseSpecification<PurchasesOrder>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId): base(o =>o.PaymentAttemptId == paymentIntentId) 
        {
        }
    }
}
