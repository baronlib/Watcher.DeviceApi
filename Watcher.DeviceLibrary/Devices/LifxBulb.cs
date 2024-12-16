using Lifx;
using System.Net;

namespace Watcher.DeviceLibrary.Devices;

public class LifxBulb : IDevice
{
    public string Name { get; set; }

    public string UniqueId { get; set; }

    public string Type => "Lifx Bulb";

    public string IpAddress { get; set; }

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