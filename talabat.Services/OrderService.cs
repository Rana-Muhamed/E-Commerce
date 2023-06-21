using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.Repositories;
using talabat.Core.Services;
using talabat.Core.Specifications.Order_Spec;

namespace talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            //IGenericRepository<Product> productsRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            //IGenericRepository<Order> ordersRepo
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productsRepo;
            //_deliveryMethodsRepo = deliveryMethodsRepo;
            //_ordersRepo = ordersRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliverMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var productsRepo = _unitOfWork.Repository<Product>();
                    if (productsRepo != null)
                    {
                        var product = await productsRepo.GetByIdAsync(item.Id) ?? null;
                        if (product != null)
                        {
                            var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                            var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                            orderItems.Add(orderItem);
                        }
                    }
                }
            }
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            DeliveryMethod deliveryMethod = new DeliveryMethod();
            var deliveryMethodRepo = _unitOfWork.Repository<DeliveryMethod>();
            if(deliveryMethodRepo != null)
             deliveryMethod = await deliveryMethodRepo.GetByIdAsync(deliverMethodId);
            var spec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetByEntityWithSpecAsync(spec);
            if(existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal, basket.PaymentIntentId);
            var ordersRepo= _unitOfWork.Repository<Order>();
            if (ordersRepo != null)
            {
                await ordersRepo.Add(order);
                var result = await _unitOfWork.Compelete();
                if (result > 0)
                    return order;
            }
            return null;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail,orderId);
            var order = await _unitOfWork.Repository<Order>().GetByEntityWithSpecAsync(spec);
            if(order is null) return null;
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
