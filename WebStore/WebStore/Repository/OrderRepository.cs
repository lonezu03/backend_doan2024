
using System;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
namespace WebStore.Repository
{
    public class OrderRepository : IOrderRepository
        {
            private readonly ApplicationDbContext _context;


            public OrderRepository (ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<Orders>> GetAllOrdersAsync()
            {
                return await _context.Orders
                    //.Include(o => o.User)
                    //.Include(o => o.Shipping)
                    .ToListAsync();
            }
            public async Task<Orders> GetOrderByIdAsync(int orderId)
            {
                // Logic để lấy dữ liệu order từ cơ sở dữ liệu
                return await _context.Orders.FindAsync(orderId);
            }
            public async Task<Orders> CreateOrderAsync(Orders order)
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    return order;
                }
            public async Task<bool> UpdateOrderAsync(Orders order)
            {
                // Kiểm tra nếu order null
                if (order == null)
                    throw new ArgumentNullException(nameof(order), "Order cannot be null.");

                // Kiểm tra sự tồn tại của đơn hàng trong cơ sở dữ liệu
                var existingOrder = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == order.Id);
                if (existingOrder == null)
                    return false;

                // Cập nhật thông tin đơn hàng
                _context.Orders.Update(order);

                // Lưu thay đổi
                return await _context.SaveChangesAsync() > 0;
            }   

            public async Task<bool> DeleteOrderAsync(int id)
                {
                    var order = await _context.Orders.FindAsync(id);
                    if (order == null) return false;

                    _context.Orders.Remove(order);
                    return await _context.SaveChangesAsync() > 0;
                }
            }   
 }
