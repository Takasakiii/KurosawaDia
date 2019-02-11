using Bot.Nucleo.Extensions;
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

        public async Task Avatar(DiscordSocketClient client)
        {
            ulong id = 0;
            try
            {
                id = Convert.ToUInt64(context.Message.MentionedUserIds);
            } catch
            {
                id = context.User.Id;
            }

            Discord.WebSocket.SocketUser user = client.GetUser(id);

            string avatarUrl = "";
            if(user.GetAvatarUrl() != null)
            {
                avatarUrl = user.GetAvatarUrl(0, 2048);
            } else
            {
                avatarUrl = user.GetDefaultAvatarUrl();
            }

            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(avatarUrl)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
