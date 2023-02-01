using EcommerceAPI.Model.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Model
{
    public interface IPurchaseOrderService
    {
        Task<PurchasesOrder> AddPurchaseOrderAsync(string buyerEmail, int shippingType, string cartId, Model.PurchaseOrder.Address address);

        Task<IReadOnlyList<PurchasesOrder>> GetPurchasesOrderByUserEmailAsync(string email);

        Task<PurchasesOrder> GetPurchasesOrderByIdAsync(int id);

        Task<IReadOnlyList<ShippingType>> GetShippingType();

    }
}
