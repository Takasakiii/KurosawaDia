using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

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
            //eh na gambiarra mas eh o melhor q consigo agr 
            //perdoa nois Takasaki S2
            string UserId = "";
            
            try
            {
                string[] msgArr = context.Message.Content.Split(' ');
                UserId = msgArr[1].Replace("<", "").Replace("!", "").Replace("@", "").Replace(">", "");
            } catch
            {
                UserId = context.User.Id.ToString();
            }

            Discord.WebSocket.SocketUser user = client.GetUser(Convert.ToUInt64(UserId));

            string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

            EmbedBuilder builder = new EmbedBuilder()
                .WithAuthor($"{user.Username}#{user.Discriminator}")
                .WithDescription($"[Link Direto]({avatarUrl})")
                .WithImageUrl(avatarUrl)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
