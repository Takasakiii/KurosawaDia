using Bot.Extensions;
using Discord;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Nsfw : Weeb
    {
        public void hentai(CommandContext context, object[] args)
        {
            ITextChannel canal = context.Channel as ITextChannel;
            if (context.IsPrivate || canal.IsNsfw)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithImageUrl(new HttpExtensions().GetSite("https://nekobot.xyz/api/image?type=hentai", "message")) //¯\_(ツ)_/¯
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** você so pode usar esse comando em um canal NSFW")
                        .WithColor(Color.Red)
                    .Build());
            }
        }
    }
}
