using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
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
    }


}
