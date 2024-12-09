using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebStore.Context;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service;
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
                // Giả định: Có thông tin user_id thông qua xác thực

                int userId = _userService.GetCurrentUserId();
                if (model.variantid == 0)
                    return StatusCode(500, new { Message = "bang 0 roi ni oi" });
                Console.WriteLine($"variantid: {model.variantid}, quantity: {model.quantity}, price: {model.price}");

                // 1. Tìm giỏ hàng chưa hoàn tất của người dùng
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.User_Id == userId && o.status == "pending");

                // 2. Nếu không tồn tại, tạo mới giỏ hàng
                if (order == null)
                {
                    order = new Orders
                    {
                        User_Id = userId,
                        Date = DateTime.Now,
                        status = "pending",
                        total_amount = 0 // Ban đầu chưa có tổng tiền

                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync(); // Lưu để lấy ID giỏ hàng
                }

                // 3. Kiểm tra sản phẩm trong giỏ hàng
                var orderItem = await _context.Order_Item
                    .FirstOrDefaultAsync(oi =>
                            oi.Order_Id == order.Id &&
                            oi.variant_id == model.variantid &&
                            oi.name == model.name &&
                            oi.size == model.size &&

                            oi.color == model.color);

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
                        quantity = model.quantity,
                        color = model.color,
                        imagesp = model.image,
                        size = model.size,
                        name = model.name,
                        price = model.price,

                    };
                    _context.Order_Item.Add(orderItem);
                }
                order.total_amount = await _context.Order_Item
                    .Where(oi => oi.Order_Id == order.Id)
                    .SumAsync(oi => oi.quantity * model.price);
                // 4. Lưu thay đổi
                await _context.SaveChangesAsync();

                // 5. Trả về danh sách sản phẩm trong giỏ hàng
                var cartItems = await _context.Order_Item
                    .Where(oi => oi.Id == order.Id)
                    .Select(oi => new
                    {
                        oi.Inventory_Id,
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
            public string? name { get; set; }
            public string? image { get; set; }
            public string? color { get; set; }
            public string? size { get; set; }

        }
    }
}
