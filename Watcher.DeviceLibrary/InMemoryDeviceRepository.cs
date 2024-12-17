using System.Text.Json;
using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly Dictionary<string, IDevice> _idToDeviceDictionary = new();

    public InMemoryDeviceRepository()
    {
        var jsonFile = File.ReadAllBytes("devices.json");
        var options = new JsonSerializerOptions
        {
            Converters = { new DeviceConverter() }
        };

        var devices = JsonSerializer.Deserialize<List<IDevice>>(jsonFile, options);
        if (devices != null)
        {
            _idToDeviceDictionary = devices.ToDictionary(d => d.Id);
        }
    }

    public IDevice? Get(string uniqueId)
    {
        return _idToDeviceDictionary.GetValueOrDefault(uniqueId);
    }
}