using EcommerceAPI.Interfaces;
using EcommerceAPI.Model;
using EcommerceAPI.Model.PurchaseOrder;
using EcommerceAPI.Specification;
using Stripe;
using Product = EcommerceAPI.Model.Product;

namespace EcommerceAPI.Logic
{
    public class PaymentService : IPaymentService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(cartId);
            if (shoppingCart == null) return null;

            var shippingTypePrice = 0m;
            if (shoppingCart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<ShippingType>()
                    .GetByIdAsync((int)shoppingCart.DeliveryMethodId);
            }
            foreach (var item in shoppingCart.Items)
            {
                var productItem =  await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(shoppingCart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)shoppingCart.Items.Sum(i => i.Amount * (i.Price * 100)) + (long)shippingTypePrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                shoppingCart.PaymentIntentId = intent.Id;
                shoppingCart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)shoppingCart.Items.Sum(i => i.Amount * (i.Price * 100)) + (long)shippingTypePrice * 100,

                };
                await service.UpdateAsync(shoppingCart.PaymentIntentId, options);
            }
            await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
            return shoppingCart;
        }

        public async Task<PurchasesOrder> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<PurchasesOrder>().GetByIdWithSpec((ISpecification<PurchasesOrder>)spec);

            if (order == null) return null;
            order.Status = StatusOrder.FailedPayment;
            await _unitOfWork.Complete();

            return order;
        }
        public async Task<PurchasesOrder> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<PurchasesOrder>().GetByIdWithSpec((ISpecification<PurchasesOrder>)spec);

            if (order == null) return null;

            order.Status = StatusOrder.PaymentReceived;
            _unitOfWork.Repository<PurchasesOrder>()?.Update(order);

            await _unitOfWork.Complete();

            return order;
        }
    }
}
