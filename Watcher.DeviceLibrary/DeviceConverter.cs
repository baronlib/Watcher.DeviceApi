using System.Text.Json;
using System.Text.Json.Serialization;
using Watcher.DeviceLibrary.Devices;

namespace Watcher.DeviceLibrary;

public class DeviceConverter : JsonConverter<IDevice>
{
    public override IDevice? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var type = root.GetProperty("Type").GetString();

        return type switch
        {
            "LIFX Color 1000" => JsonSerializer.Deserialize<LifxBulb>(root.GetRawText(), options),
            "TP-Link HS100" => JsonSerializer.Deserialize<TpLinkPowerPoint>(root.GetRawText(), options),
            _ => throw new NotSupportedException($"Device type '{type}' is not supported")
        };
    }

    public override void Write(Utf8JsonWriter writer, IDevice value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}