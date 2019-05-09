using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Comandos
{
    public class Owner : Ajuda
    {
        public void ping(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($" meu ping é {client.Latency}ms") //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                .Build());
        }
    }
}
