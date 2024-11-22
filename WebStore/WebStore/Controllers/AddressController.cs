using Microsoft.AspNetCore.Mvc;
using WebStore.Entity;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound(new { Message = "Address not found" });
            }

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Address address)
        {
            await _addressService.AddAsync(address);
            return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _addressService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
