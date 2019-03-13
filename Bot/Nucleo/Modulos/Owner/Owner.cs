using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Modulos.Owner
{
    public class Owner
    {
        CommandContext context;

        public Owner(CommandContext context)
        {
            this.context = context;
        }

        public async Task Ping()
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;
            EmbedBuilder builder = new EmbedBuilder()
                 .WithDescription($" meu ping é {client.Latency}ms");
           Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
