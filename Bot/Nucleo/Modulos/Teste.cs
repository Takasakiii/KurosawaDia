using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var sw = Stopwatch.StartNew();
            var msg = await context.Channel.SendMessageAsync("🏓").ConfigureAwait(false);
            sw.Stop();
            msg.DeleteAfter(0);

            await context.Channel.SendConfirmAsync($"{Format.Bold(context.User.ToString())} 🏓 {(int)sw.Elapsed.TotalMilliseconds}ms").ConfigureAwait(false);
        }

        public async Task Avatar(DiscordSocketClient client)
        {
            //eh na gambiarra mas eh o melhor q consigo agr 
            //perdoa nois Takasaki S2
            string UserId = "";
            
            try
            {
                UserId = context.Message.MentionedUserIds.GetFirst();
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
