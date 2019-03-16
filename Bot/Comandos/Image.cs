using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Comandos
{
    public class Image
    {
        public void neko(CommandContext context, object[] args)
        {
            string url = new HttpExtension().GetSite("https://nekos.life/api/v2/img/neko", "url").GetAwaiter().GetResult();
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Nekos")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
