using AutoMapper;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                User_Id = order.User_Id,
                Shipping_Id = order.Shipping_Id,
                Date = order.Date,
               // UserName = order.User.Username,
                //ShippingAddress = null,
            }).ToList();
        }
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                User_Id = order.User_Id,
                Shipping_Id = order.Shipping_Id,
                Date = order.Date,
               // UserName = order.User.Username,
               // ShippingAddress = null
            };
        }
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto newOrder)
        {
            var order = _mapper.Map<Orders>(newOrder);
            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }
        public async Task<bool> UpdateOrderAsync(int id, UpdateOrderDto updatedOrder)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null) return false;

            _mapper.Map(updatedOrder, existingOrder);
            return await _orderRepository.UpdateOrderAsync(existingOrder);
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
