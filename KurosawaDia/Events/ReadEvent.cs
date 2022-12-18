using Discord;
using Discord.WebSocket;
using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Events;

public class ReadEvent : IAutoStartService
{
    private readonly IDiscordClient _client;

    public ReadEvent(IDiscordClient client)
    {
        _client = client;
    }

    public void Activate()
    {
        if (_client is DiscordSocketClient socketClient)
        {
            socketClient.Ready += HandlerReady;
        }
    }

    private async Task HandlerReady()
    {
        var socketClient = _client as DiscordSocketClient;
        if (socketClient is null) return;

        await socketClient.SetActivityAsync(new Game("KurosawaDia", ActivityType.Watching));
    }
}
