using Microsoft.AspNetCore.Mvc;
using Watcher.DeviceLibrary;

namespace Watcher.DeviceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController(IDeviceRepository deviceRepository) : ControllerBase
    {
        [HttpPost("turn-on")]
        public async Task<IActionResult> TurnOn([FromQuery] string uniqueId, [FromQuery] TimeSpan? duration = null, [FromQuery] bool onlyIfDark = false)
        {
            IDevice? device = await deviceRepository.Get(uniqueId);
            if (device == null)
            {
                return NotFound($"Device with UniqueId {uniqueId} not found.");
            }

            if (duration.HasValue)
            {
                if (duration.Value.TotalSeconds < 1)
                {
                    return BadRequest("Duration must be at least 1 second.");
                }

                await device.TurnOn(duration.Value);
            }
            else
            {
                await device.TurnOn();
            }

            return Ok($"Device {device.Name} turned on.");
        }

        [HttpPost("turn-off")]
        public async Task<IActionResult> TurnOff([FromQuery] string uniqueId)
        {
            IDevice? device = await deviceRepository.Get(uniqueId);
            if (device == null)
            {
                return NotFound($"Device with UniqueId {uniqueId} not found.");
            }

            await device.TurnOff();
            return Ok($"Device {device.Name} turned off.");
        }
    }
}