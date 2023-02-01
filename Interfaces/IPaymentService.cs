
using EcommerceAPI.Model;
using EcommerceAPI.Model.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces
{
    public interface IPaymentService
    {
        Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId);
        Task<PurchasesOrder> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<PurchasesOrder> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
