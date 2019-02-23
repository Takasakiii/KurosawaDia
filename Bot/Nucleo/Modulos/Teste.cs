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

            await context.SendConfirmAsync($"🏓 {(int)sw.Elapsed.TotalMilliseconds}ms").ConfigureAwait(false);
        }

        public async Task Avatar(DiscordSocketClient client, string[] comando)
        {
            //eh na gambiarra mas eh o melhor q consigo agr 
            //perdoa nois Takasaki S2

            SocketUser user = context.GetUser(client, comando);

            string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

            EmbedBuilder builder = new EmbedBuilder()
                .WithAuthor($"{user}")
                .WithDescription($"[Link Direto]({avatarUrl})")
                .WithImageUrl(avatarUrl)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
