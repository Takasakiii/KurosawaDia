using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo.Modulos
{
    public class WeebCmds
    {
        CommandContext context;

        public WeebCmds(CommandContext context)
        {
            this.context = context;
        }

        public async Task hug(WeebClient weebClient)
        {
            var img = await weebClient.GetRandomAsync("hug", new string[] { }, FileType.Gif, false, NsfwSearch.False);

            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(img.Url)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);
        }
    }
}
