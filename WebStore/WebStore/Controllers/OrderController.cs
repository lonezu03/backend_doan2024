using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public OrderController(IOrderService orderService, IUserService userService, ApplicationDbContext context)
        {
            _orderService = orderService;
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto newOrder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdOrder = await _orderService.CreateOrderAsync(newOrder);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] UpdateOrderDto updatedOrder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _orderService.UpdateOrderAsync(id, updatedOrder);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] OrderItemModel model)
        {
            try
            {
                // Lấy userId từ dịch vụ UserService
                int userId = _userService.GetCurrentUserId();

                if (model.variantid <= 0)
                    return BadRequest(new { Message = "Variant ID phải lớn hơn 0" });

                // Tìm giỏ hàng chưa hoàn tất của người dùng
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.User_Id == userId && o.status == "pending");

                // Nếu không tồn tại, tạo mới giỏ hàng
                if (order == null)
                {
                    order = new Orders
                    {
                        User_Id = userId,
                        Date = DateTime.Now,
                        status = "pending",
                        total_amount = 0
                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync(); // Lưu để lấy ID giỏ hàng
                }

                // Kiểm tra sản phẩm trong giỏ hàng
                var orderItem = await _context.Order_Item
                    .FirstOrDefaultAsync(oi => oi.Order_Id == order.Id && oi.variant_id == model.variantid);

                if (orderItem != null)
                {
                    // Nếu sản phẩm đã tồn tại, tăng số lượng
                    orderItem.quantity += model.quantity;
                }
                else
                {
                    // Nếu chưa, thêm mới sản phẩm vào giỏ hàng
                    orderItem = new Order_Item
                    {
                        Order_Id = order.Id,
                        variant_id = model.variantid,
                        quantity = model.quantity
                    };
                    _context.Order_Item.Add(orderItem);
                }

                // Tính tổng tiền của giỏ hàng
                order.total_amount = await _context.Order_Item
                    .Where(oi => oi.Order_Id == order.Id)
                    .SumAsync(oi => oi.quantity * model.price);

                // Lưu thay đổi
                await _context.SaveChangesAsync();

                // Trả về danh sách sản phẩm trong giỏ hàng
                var cartItems = await _context.Order_Item
                    .Where(oi => oi.Order_Id == order.Id)
                    .Select(oi => new
                    {
                        oi.variant_id,
                        oi.quantity
                    }).ToListAsync();

                return Ok(new { CartItems = cartItems });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        public class OrderItemModel
        {
            public int variantid { get; set; }
            public int quantity { get; set; }
            public decimal price { get; set; }
        }
    }
}
