namespace Watcher.DeviceLibrary;

public interface IDeviceRepository
{
    Task<IDevice?> Get(string uniqueId);
}