namespace Watcher.DeviceLibrary;

public interface IDevice
{
    string Id { get; set; }

    string Description { get; set; }

    string Type { get; }

    Task TurnOn();

    Task TurnOff();
}