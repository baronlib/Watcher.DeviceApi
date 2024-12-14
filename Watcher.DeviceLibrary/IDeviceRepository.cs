namespace Watcher.DeviceLibrary;

public interface IDeviceRepository
{
    Task Add(IDevice device);

    Task Remove(string uniqueId);

    Task Update(IDevice device);

    Task<IDevice?> Get(string uniqueId);

    Task<IEnumerable<IDevice>> GetAll();
}