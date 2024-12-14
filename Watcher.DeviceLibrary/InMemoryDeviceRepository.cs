﻿using System.Linq;
using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly List<IDevice> _devices =
    [
        new LifxBulb("192.168.1.27")
        {
            Name = "Front porch light",
            UniqueId = "front-light"
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