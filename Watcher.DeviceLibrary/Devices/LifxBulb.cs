using LifxNet;

namespace Watcher.DeviceLibrary.Devices;

public class LifxBulb(string hostName) : IDevice
{
    private LifxClient? _client;

    private readonly LightBulb _lightBulb = new(hostName, []);
    
    public async Task EnsureInitialised()
    {
        // TODO - not thread safe!
        if (_client == null)
        {
            _client = await LifxClient.CreateAsync();
        }
    }

    public string Name { get; set; }

    public string UniqueId { get; set; }

    public string Type => "Lifx Bulb";

    public async Task TurnOn()
    {
        await EnsureInitialised();

        if (_client != null)
        {
            await _client.TurnBulbOnAsync(_lightBulb, TimeSpan.FromSeconds(1));
        }
    }

    public async Task TurnOff()
    {
        await EnsureInitialised();

        if (_client != null)
        {
            await _client.TurnBulbOffAsync(_lightBulb, TimeSpan.FromSeconds(5));
        }
    }
}