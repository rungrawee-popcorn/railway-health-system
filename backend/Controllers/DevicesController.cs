using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceService _service;

        public DevicesController(DeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _service.GetAll();
            return Ok(devices);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody] Device device)
        {
            await _service.Create(device);
            return Ok("Created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] Device device)
        {
            device.Id = id;
            await _service.Update(device);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            await _service.Delete(id);
            return Ok("Deleted");
        }
    }
}