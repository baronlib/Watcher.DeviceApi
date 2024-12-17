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

        /// <summary>
        /// Turns on the device with the specified unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID of the device.</param>
        /// <param name="duration">The duration for which the device should remain on. e.g. 00:01:10 for 1 minute and 10 seconds</param>
        /// <param name="onlyAfterDark">Only turn on if after dark</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns a message indicating the device was turned on.</response>
        /// <response code="400">If the duration is less than 1 second.</response>
        /// <response code="404">If the device with the specified unique ID is not found.</response>
        [HttpPost("turn-on/{uniqueId}")]
        public async Task<IActionResult> TurnOn(string uniqueId, [FromQuery] TimeSpan? duration = null, [FromQuery] bool onlyAfterDark = false)
        {
            IDevice? device = await deviceRepository.Get(uniqueId);
            if (device == null)
            {
                return NotFound($"Device with UniqueId {uniqueId} not found.");
            }

            if (onlyAfterDark)
            {
                var c = new Coordinate(_location.Latitude, _location.Longitude, DateTime.UtcNow);
                Celestial cel = c.CelestialInfo;
                if (cel.IsSunUp)
                {
                    return Ok($"Device {device.Description} NOT turned on, as the sun is up.");
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

            return Ok($"Device {device.Description} turned on.");
        }

        /// <summary>
        /// Turns off the device with the specified unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID of the device.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Returns a message indicating the device was turned off.</response>
        /// <response code="404">If the device with the specified unique ID is not found.</response>
        [HttpPost("turn-off/{uniqueId}")]
        public async Task<IActionResult> TurnOff(string uniqueId)
        {
            IDevice? device = await deviceRepository.Get(uniqueId);
            if (device == null)
            {
                return NotFound($"Device with UniqueId {uniqueId} not found.");
            }

            await device.TurnOff();
            return Ok($"Device {device.Description} turned off.");
        }
    }
}