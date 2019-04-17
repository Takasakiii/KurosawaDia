using Bot.Comandos;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace Bot.Nucleo.Modulos.Owner
{
    public class Owner : Ajuda
    {
        public void ping(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($" meu ping é {client.Latency}ms") //pedreragem top
                .Build());
        }

    }
}
