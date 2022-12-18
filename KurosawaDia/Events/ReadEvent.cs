using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Events;

public class ReadEvent : IAutoStartService
{
    private readonly IDiscordClient _client;
    private readonly InteractionService _interactions;
    private readonly IBotConfigService _config;

    public ReadEvent(IDiscordClient client, InteractionService interactions, IBotConfigService config)
    {
        _client = client;
        _interactions = interactions;
        _config = config;
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

        await socketClient.SetActivityAsync(new Game($"Carregado {_interactions.SlashCommands.Count()}", ActivityType.Watching));

#if DEBUG
        await _interactions.RegisterCommandsToGuildAsync(_config.MainGuild, true);
#else
        await _interactions.RegisterCommandsGloballyAsync(true);
#endif
    }
}
