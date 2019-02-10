using Discord;
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
            string animo = "";
            Discord.Color color = new Color();

            if(client.Latency < 200)
            {
                animo = "Eu estou animada pro trabalho 😃";
                color = new Color(Constants.cor);
            } else
            {
                animo = "Eu estou triste para o trabalho 😔";
                color = new Color(Constants.red);
            }

            EmbedBuilder builder = new EmbedBuilder()
                .WithTitle(animo)
                .WithDescription($"**{context.User.Username}#{context.User.Discriminator}** 🏓 meu ping eh {client.Latency}ms")
                .WithColor(color);
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
