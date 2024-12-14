namespace Watcher.DeviceLibrary;

public interface IDevice
{
    string Name { get; set; }

    string UniqueId { get; set; }

    string Type { get; }

    Task TurnOn();

    Task TurnOff();
}