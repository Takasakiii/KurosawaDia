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

            await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($" meu ping é {client.Latency}ms")
                .Build());
        }
    }
}
