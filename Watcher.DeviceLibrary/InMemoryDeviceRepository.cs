using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly List<IDevice> _devices =
    [
        new LifxBulb
        {
            Description = "Front porch light",
            Id = "front-light",
            IpAddress = "192.168.1.27"
        },
        new TpLinkPowerPoint
        {
            Description = "LED Flood Light",
            Id = "power-point",
            Ip = "192.168.1.4"
        }
    ];

    public Task<IDevice?> Get(string uniqueId)
    {
        var device = _devices.FirstOrDefault(d => d.Id == uniqueId);
        return Task.FromResult(device);
    }
}