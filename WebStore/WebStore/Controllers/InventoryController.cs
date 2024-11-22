using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _inventoryService.GetAllAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _inventoryService.GetByIdAsync(id);
            if (inventory == null)
            {
                return NotFound(new { Message = "Inventory not found" });
            }

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InventoryDTO inventoryDto)
        {
            await _inventoryService.AddAsync(inventoryDto);
            return Ok(new { Message = "Inventory added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InventoryDTO inventoryDto)
        {
            if (id != inventoryDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch" });
            }

            await _inventoryService.UpdateAsync(inventoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _inventoryService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
