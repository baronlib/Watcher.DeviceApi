using Microsoft.AspNetCore.Mvc;
using System;
using CoordinateSharp;
using Watcher.DeviceLibrary;
using Microsoft.Extensions.Options;
using Watcher.DeviceApi.Models;

namespace Watcher.DeviceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController(
        IDeviceRepository deviceRepository,
        IDeviceTimer deviceTimer,
        IOptions<Location> locationOptions) : ControllerBase
    {
        private readonly Location _location = locationOptions.Value;

        [HttpPost("turn-on/{uniqueId}")]
        public async Task<IActionResult> TurnOn(string uniqueId, [FromQuery] TimeSpan? duration = null, [FromQuery] bool onlyIfDark = false)
        {
            IDevice? device = await deviceRepository.Get(uniqueId);
            if (device == null)
            {
                return NotFound($"Device with UniqueId {uniqueId} not found.");
            }

            if (onlyIfDark)
            {
                var c = new Coordinate(_location.Latitude, _location.Longitude, DateTime.UtcNow);
                Celestial cel = c.CelestialInfo;
                if (cel.IsSunUp)
                {
                    return Ok($"Device {device.Name} NOT turned on, as the sun is up.");
                }
            }

            if (duration.HasValue)
            {
                if (duration.Value.TotalSeconds < 1)
                {
                    return BadRequest("Duration must be at least 1 second.");
                }

                await deviceTimer.TurnOn(device, duration.Value);
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