
using EcommerceAPI.Interfaces;
using EcommerceAPI.Model;
using EcommerceAPI.Model.PurchaseOrder;
using EcommerceAPI.Specification;

using Stripe;
using Product = EcommerceAPI.Model.Product;

namespace EcommerceAPI.Logic
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public PurchaseOrderService(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<PurchasesOrder> AddPurchaseOrderAsync(string buyerEmail, int shippingType, string cartId, Model.PurchaseOrder.Address address)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(cartId);

            var items = new List<OrderItem>();
            foreach (var item in shoppingCart.Items)
            {
                var itemProduct = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var orderedItem = new OrderedProductItem(itemProduct.Id, itemProduct.Name, itemProduct.Image);
                var itemOrder = new OrderItem(orderedItem, itemProduct.Price, item.Amount);
                items.Add(itemOrder);
            }

            // get delivery method from repo
            var shippingTypeEntity = await _unitOfWork.Repository<ShippingType>().GetByIdAsync(shippingType);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Amount);

            // check to see if order exits
            var spec = new OrderByPaymentIntentIdSpecification(shoppingCart.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<PurchasesOrder>().GetByIdWithSpec(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<PurchasesOrder>().DeleteEntity(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(shoppingCart.PaymentIntentId);
            }

            //create order
            var purchasesOrder = new PurchasesOrder(buyerEmail, address, shippingTypeEntity, items, subtotal, shoppingCart.PaymentIntentId);

            //add to database
            _unitOfWork.Repository<PurchasesOrder>().AddEntity(purchasesOrder);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            // return order
            return purchasesOrder;
        }


        public async Task<PurchasesOrder> GetPurchasesOrderByIdAsync(int id)
        {
            var spec = new PurchaseOrderWithItemsSpecification(id);
            return await _unitOfWork.Repository<PurchasesOrder>().GetByIdWithSpec(spec);
        }

        public async Task<IReadOnlyList<PurchasesOrder>> GetPurchasesOrderByUserEmailAsync(string email)
        {
            var spec = new PurchaseOrderWithItemsSpecification(email);
            return await _unitOfWork.Repository<PurchasesOrder>().GetAllWithSpec(spec);
        }

        public async Task<IReadOnlyList<ShippingType>> GetShippingType()
        {
            return await _unitOfWork.Repository<ShippingType>().GetAllAsync();
        }
    }
}
