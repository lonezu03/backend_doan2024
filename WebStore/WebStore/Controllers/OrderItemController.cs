using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebStore.Context;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public OrderItemController(IOrderItemService orderItemService, IUserService userService,ApplicationDbContext context )
        {
            _orderItemService = orderItemService;
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _orderItemService.GetAllAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound(new { Message = "Order item not found" });
            }

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderItemDTO orderItemDto)
        {
            await _orderItemService.AddAsync(orderItemDto);
            return Ok(new { Message = "Order item added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderItemDTO orderItemDto)
        {
            Console.WriteLine($"Payload: {JsonConvert.SerializeObject(orderItemDto)}");

            if (id != orderItemDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch" });
            }

            await _orderItemService.UpdateAsync(orderItemDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _orderItemService.DeleteByIdAsync(id);
            return NoContent();
        }
        [HttpGet("get-order-items")]
        public async Task<IActionResult> GetOrderItems()
        {
            try
            {
                // Lấy userId từ token
                int userId = _userService.GetCurrentUserId();

                // Lấy danh sách đơn hàng của người dùng
                var orders = await _context.Orders
                    .Where(o => o.User_Id == userId)
                    .Select(o => o.Id)
                    .ToListAsync();

                if (!orders.Any())
                {
                    return Ok(new { Message = "No orders found for this user." });
                }

                // Lấy danh sách các Order_Item
                var orderItems = await _context.Order_Item
                    .Where(oi => orders.Contains(oi.Order_Id))
                    .Select(oi => new
                    {
                        oi.Id,
                        oi.Order_Id,
                        oi.Inventory_Id,
                        oi.variant_id,
                        oi.status,
                        oi.imagesp,
                        oi.color,
                        oi.size,
                        oi.quantity,
                        oi.price,
                        oi.name,
                    })
                    .ToListAsync();

                return Ok(new { OrderItems = orderItems });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
   



}
