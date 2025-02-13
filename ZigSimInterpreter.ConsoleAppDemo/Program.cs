using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ZigSimInterpreter.ConsoleAppDemo;

internal class Program
{
    static readonly int port = 50000;
    static string rawJson = "";
    static ZigSimResult? zigSimResult = null;
    static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        var task = ReceiveZigSim(cts.Token);
        Console.WriteLine("Press 'Q' to quit, 'C' to show the latest ZigSim data");
        int startRow = Console.GetCursorPosition().Top;

        while (true)
        {
            // type 'Q' to quit
            var cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Q)
            {
                Console.WriteLine("Cancelling...");
                cts.Cancel();
                break;
            }
            if (cki.Key == ConsoleKey.C)
            {
                ClearFromLine(startRow);
                Console.WriteLine(rawJson);

                if (zigSimResult != null)
                {
                    if (zigSimResult.IsSuccess)
                    {
                        Console.WriteLine("Deserialized ZigSimPayload:");
                        Console.WriteLine(JsonSerializer.Serialize(zigSimResult.Payload, new JsonSerializerOptions() { WriteIndented = true }));
                    }
                    else
                    {
                        Console.WriteLine($"Error: {zigSimResult.ErrorMessage}");
                    }

                }
            }
        }

        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Task was cancelled");
        }
    }

    static async Task ReceiveZigSim(CancellationToken cts)
    {
        var interpreter = new ZigSimJsonInterpreter();
        using (var udpClient = new UdpClient(port))
        {
            while (true)
            {
                var receivedResults = await udpClient.ReceiveAsync(cts);
                var receivedText = Encoding.UTF8.GetString(receivedResults.Buffer);
                zigSimResult = interpreter.Read(receivedResults.Buffer);
                rawJson = PrettyJson(receivedText);
            }
        }
    }
    static void ClearFromLine(int startRow)
    {
        int currentRow = Console.GetCursorPosition().Top;
        for (int row = startRow; row <= currentRow; row++)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(new string(' ', Console.WindowWidth)); // 空白で上書き
        }
        Console.SetCursorPosition(0, startRow); // カーソルを戻す
    }

    static string PrettyJson(string unPrettyJson)
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

        return JsonSerializer.Serialize(jsonElement, options);
    }
}
