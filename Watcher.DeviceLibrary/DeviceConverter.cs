using System.Text.Json;
using System.Text.Json.Serialization;
using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class DeviceConverter : JsonConverter<IDevice>
{
    private readonly Dictionary<string, Type> _deviceTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t => typeof(IDevice).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .ToDictionary(t => t.GetProperty("Type")?.GetValue(Activator.CreateInstance(t))?.ToString() ?? string.Empty);
    
    public override IDevice? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var type = root.GetProperty("Type").GetString();

        if (type != null)
        {
            if (_deviceTypes.TryGetValue(type, out var deviceType))
            {
                return (IDevice?)JsonSerializer.Deserialize(root.GetRawText(), deviceType, options);
            }
        }

        throw new NotSupportedException($"Device type '{type}' is not supported");
    }

    public override void Write(Utf8JsonWriter writer, IDevice value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}