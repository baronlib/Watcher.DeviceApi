using System.Net.Sockets;

namespace Watcher.DeviceLibrary.Devices;

public class TpLinkPowerPoint() : IDevice
{
    public string Ip { get; set; } = string.Empty;

    private const int Port = 9999;

    private const string OffCommand = "AAAAKtDygfiL/5r31e+UtsWg1Iv5nPCR6LfEsNGlwOLYo4HyhueT9tTu3qPeow==";
    private const string OnCommand = "AAAAKtDygfiL/5r31e+UtsWg1Iv5nPCR6LfEsNGlwOLYo4HyhueT9tTu36Lfog==";
    private const string QueryCommand = "AAAAI9Dw0qHYq9+61/XPtJS20bTAn+yV5o/hh+jK8J7rh+vLtpbr";

    private void SendCommand(string command)
    {
        try
        {
            var client = new TcpClient(Ip, Port);
            Byte[] data = Convert.FromBase64String(command);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            Span<Byte> reply = new Span<byte>();
            stream.ReadExactly(reply);

            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unable to send command to TPLink PowerPoint");
        }
    }

    public string Name { get; set; } = string.Empty;

    public string UniqueId { get; set; } = Guid.Empty.ToString();

    public string Type => "TPLink Power Point";

    public Task TurnOn()
    {
        SendCommand(OnCommand);
        return Task.CompletedTask;
    }

    public Task TurnOff()
    {
        SendCommand(OffCommand);
        return Task.CompletedTask;
    }
}