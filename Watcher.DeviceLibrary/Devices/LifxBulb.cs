using Lifx;
using System.Net;

namespace Watcher.DeviceLibrary.Devices;

public class LifxBulb : IDevice
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type => "Lifx Bulb";
    public string IpAddress { get; set; } = string.Empty;

    private async Task<ILight> Connect()
    {
        var lightFactory = new LightFactory();
        return await lightFactory.CreateLightAsync(IPAddress.Parse(IpAddress));
    }

    public async Task TurnOn()
    {
        using var light = await Connect();

        await light.SetPowerAsync(Power.On);
    }

    public async Task TurnOff()
    {
        using var light = await Connect();

        await light.SetPowerAsync(Power.Off);
    }
}