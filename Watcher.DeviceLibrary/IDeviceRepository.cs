namespace Watcher.DeviceLibrary;

public interface IDeviceRepository
{
    IDevice? Get(string uniqueId);
}