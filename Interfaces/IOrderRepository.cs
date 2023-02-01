using EcommerceAPI.Model;

namespace EcommerceAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<Orders> AddPurchaseOrder(int id, string user_id);
        Task<Orders> getOrderById();
    }
}
