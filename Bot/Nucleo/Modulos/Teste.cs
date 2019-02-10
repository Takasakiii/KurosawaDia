using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Nucleo.Modulos
{
    public class Teste
    {
        CommandContext context;

        public Teste(CommandContext context)
        {
            this.context = context;
        }

        public async Task Ping(DiscordSocketClient client)
        {
            await context.Channel.SendMessageAsync($"🏓 meu ping eh {client.Latency}ms").ConfigureAwait(false);
        }
    }
}
