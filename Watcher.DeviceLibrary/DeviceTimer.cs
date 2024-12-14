namespace Watcher.DeviceLibrary;

public static class DeviceTimer
{
    public static async Task TurnOn(this IDevice device, TimeSpan duration)
    {
        await device.TurnOn();

        _ = Task.Run(async () =>
        {
            await Task.Delay(duration);
            await device.TurnOff();
        });
    }
}