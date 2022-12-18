using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Events;

public class InteractionCreatedEvent : IAutoStartService
{
    private readonly IDiscordClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;

    public InteractionCreatedEvent(
        IDiscordClient client,
        InteractionService interactions,
        IServiceProvider services)
    {
        _client = client;
        _interactions = interactions;
        _services = services;
    }

    public void Activate()
    {
        if (_client is DiscordSocketClient socketClient)
        {
            socketClient.InteractionCreated += HandlerEvent;
        }
    }

    private async Task HandlerEvent(SocketInteraction interaction)
    {
        if (_client is not DiscordSocketClient socketClient) return;
        var context = new SocketInteractionContext(socketClient, interaction);
        await _interactions.ExecuteCommandAsync(context, _services);
    }
}
