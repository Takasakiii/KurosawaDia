using Discord;
using Discord.Interactions;

namespace KurosawaDia.Modules;

public class UtilsModule : InteractionModuleBase
{
    [SlashCommand("say", "Pode me fazer eu construir uma mensagem para você :3")]
    public async Task SayCommand(
        [Summary("Texto", "Define o texto que o bot enviará, caso vazio posso mostrar mais opções")] string? text = null)
    {
        if (text is not null)
        {
            await ReplyAsync(text);
            return;
        }

        var constructorBuilder = new EmbedBuilder()
            .WithColor(Color.Green)
            .WithTitle("Ops parece que você não me disse o que eu devo falar :3")
            .WithDescription("Mas relaxe que eu posso te ajudar a resolver isso :3\n\nUse os controles abaixo para configurar o que devo mandar 😃")
            .Build();



        var buttons = new ComponentBuilder()
            .AddRow(new ActionRowBuilder()
            .AddComponent(new ButtonBuilder()
                .WithLabel("Mensagem")
                .WithEmote(new Emoji("🗨️"))
                .WithCustomId("utils-say-text-message")
                .WithStyle(ButtonStyle.Primary)
                .Build()))
            .Build();

        await RespondAsync(ephemeral: true, embed: constructorBuilder, components: buttons);

    }
}
