using System.Linq;
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

    public Task Add(IDevice device)
    {
        throw new NotImplementedException();
    }

    public Task Remove(string uniqueId)
    {
        throw new NotImplementedException();
    }

    public Task Update(IDevice device)
    {
        throw new NotImplementedException();
    }

    public Task<IDevice?> Get(string uniqueId)
    {
        var device = _devices.FirstOrDefault(d => d.UniqueId == uniqueId);
        return Task.FromResult(device);
    }

    public Task<IEnumerable<IDevice>> GetAll()
    {
        return Task.FromResult(_devices.AsEnumerable());
    }
}