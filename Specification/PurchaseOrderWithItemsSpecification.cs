using Core.Specification;
using EcommerceAPI.Model.PurchaseOrder;

namespace EcommerceAPI.Specification
{
    public class PurchaseOrderWithItemsSpecification : BaseSpecification<PurchasesOrder>
    {
        public PurchaseOrderWithItemsSpecification(string email): base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.ShippingType);
            AddOrderByDescending(o => o.PurchaseOrder);
        }
        public PurchaseOrderWithItemsSpecification(int id)
            : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.ShippingType);
            AddOrderByDescending(o => o.PurchaseOrder);
        }
    }
}
