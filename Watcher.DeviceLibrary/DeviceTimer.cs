using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Watcher.DeviceLibrary
{
    public class DeviceTimer(ILogger<DeviceTimer> logger) : IDeviceTimer
    {
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> CancellationTokens = new();

        /// <summary>
        /// Turns on a device for a particular duration of time, before turning it off again asynchronously.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public async Task TurnOn(IDevice device, TimeSpan duration)
        {
            // Cancel any existing duration in waiting for this device
            if (CancellationTokens.TryGetValue(device.Id, out var existingTokenSource))
            {
                await existingTokenSource.CancelAsync();
                existingTokenSource.Dispose();
            }

            // Create a new cancellation token source
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationTokens[device.Id] = cancellationTokenSource;

            await device.TurnOn();

            _ = Task.Run(async () =>
            {
                try
                {
                    // Wait the specified duration before turning off the device
                    await Task.Delay(duration, cancellationTokenSource.Token);
                    await device.TurnOff();

                    logger.LogInformation($"Device {device.Id} turned off after waiting {duration}");
                }
                catch (TaskCanceledException)
                {
                }
            });
        }
    }
}