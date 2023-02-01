using EcommerceAPI.Interfaces;
using EcommerceAPI.Model;

namespace EcommerceAPI.Logic
{
    
    public class Order : IOrderRepository
    {
        private readonly IOrderRepository _orderRepository;
        
        public Order(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Orders> AddPurchaseOrder(int id, string user_id)
        {
            var orderid = await _orderRepository.AddPurchaseOrder(id, user_id);

        }

        public Task<Orders> getOrderById()
        {
            throw new NotImplementedException();
        }
    }
}
