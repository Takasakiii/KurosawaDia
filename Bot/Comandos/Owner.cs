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

        public void teste(CommandContext context, object[] args)
        {

            EmbedBuilder embedo = new EmbedBuilder();
            embedo.WithColor(Color.DarkPurple);

            embedo.WithTitle("msgs maneiras");

            embedo.WithDescription("msg 1");
            context.Channel.SendMessageAsync(embed: embedo.Build());
            embedo.WithDescription("msg 2");
            context.Channel.SendMessageAsync(embed: embedo.Build());
        }
    }
}
