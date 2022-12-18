using Discord;
using Discord.Interactions;
using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Events;

public class SlashCommandExecutedEvent : IAutoStartService
{
    private readonly InteractionService _interactions;

    public SlashCommandExecutedEvent(InteractionService interactions)
    {
        _interactions = interactions;
    }

    public void Activate()
    {
        _interactions.SlashCommandExecuted += HandleEvent;
    }

    private async Task HandleEvent(SlashCommandInfo commandInfo, IInteractionContext context, IResult result)
    {
        if (result.IsSuccess) return;

        Console.WriteLine($"[ERROR] {commandInfo.Name} - {result.ErrorReason}");

        await Task.CompletedTask;
    }
}
