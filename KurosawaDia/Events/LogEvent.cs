using Discord;
using Discord.WebSocket;
using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Events;

public class LogEvent : IAutoStartService
{
    private readonly IDiscordClient _client;

    public LogEvent(IDiscordClient client)
    {
        _client = client;
    }

    public void Activate()
    {
        if (_client is DiscordSocketClient socketClient)
        {
            socketClient.Log += HandleEvent;
        }
    }

    public Task HandleEvent(LogMessage message)
    {
        var exception = message.Exception != null ? $"\n\t{message.Exception}" : "";
        Console.WriteLine($"[{message.Severity}] {message.Source} - {message.Message}{exception}");
        return Task.CompletedTask;
    }
}
