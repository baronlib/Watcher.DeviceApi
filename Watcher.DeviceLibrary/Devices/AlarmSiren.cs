namespace Watcher.DeviceLibrary.Devices;

public class AlarmSiren : IDevice
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type => "Alarm";

    public string SoundFile { get; set; } = string.Empty;
    public int Volume { get; set; } = 50;

    public Task TurnOn()
    {
        return Task.CompletedTask;
    }

    public Task TurnOff()
    {
        return Task.CompletedTask;
    }
}