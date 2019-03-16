using Bot.Comandos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Bot.Nucleo.Modulos.Owner
{
    public class Owner : Ajuda
    {
        public void ping(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($" meu ping é {client.Latency}ms")
                .Build());
        }
    }
}
