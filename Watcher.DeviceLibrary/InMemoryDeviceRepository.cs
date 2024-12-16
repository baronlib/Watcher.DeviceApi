using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly List<IDevice> _devices =
    [
        new LifxBulb
        {
            Name = "Front porch light",
            UniqueId = "front-light",
            IpAddress = "192.168.1.27"
        },
        new TpLinkPowerPoint
        {
            Name = "LED Flood Light",
            UniqueId = "power-point",
            Ip = "192.168.1.4"
        }
    ];

    public Task<IDevice?> Get(string uniqueId)
    {
        var device = _devices.FirstOrDefault(d => d.UniqueId == uniqueId);
        return Task.FromResult(device);
    }
}