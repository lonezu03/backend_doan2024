﻿using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<List<OrderItemDTO>> GetAllAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            return orderItems.Select(oi => new OrderItemDTO
            {
                Id = oi.Id,
                OrderId = oi.Order_Id,
                InventoryId = oi.Inventory_Id
            }).ToList();
        }

        public async Task<OrderItemDTO> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null) return null;

            return new OrderItemDTO
            {
                Id = orderItem.Id,
                OrderId = orderItem.Order_Id,
                InventoryId = orderItem.Inventory_Id,

            };
        }

        public async Task AddAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = new Order_Item
            {
                Order_Id = orderItemDto.OrderId,
                Inventory_Id = orderItemDto.InventoryId
            };

            await _orderItemRepository.AddAsync(orderItem);
            await _orderItemRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItemDTO orderItemDto)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(orderItemDto.Id);
            if (orderItem != null)
            {
                orderItem.Order_Id = orderItemDto.OrderId;
                orderItem.Inventory_Id = orderItemDto.InventoryId;

                await _orderItemRepository.UpdateAsync(orderItem);
                await _orderItemRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _orderItemRepository.DeleteByIdAsync(id);
            await _orderItemRepository.SaveChangesAsync();
        }
    }


}