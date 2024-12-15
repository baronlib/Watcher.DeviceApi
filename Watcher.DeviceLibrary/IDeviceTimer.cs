namespace Watcher.DeviceLibrary;

public interface IDeviceTimer
{
    Task TurnOn(IDevice device, TimeSpan duration);
}