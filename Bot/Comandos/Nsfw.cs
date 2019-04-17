using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Weeb
    {
        private void nsfw(CommandContext context, int quantidade, string url, string get)
        {
            ITextChannel canal = context.Channel as ITextChannel;
            if (context.IsPrivate || canal.IsNsfw)
            {
                HttpExtensions http = new HttpExtensions();

                if(quantidade <= 1)
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithImageUrl(http.GetSite(url, get))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    string txt = "";
                    for (int i = 0; i < quantidade; i++)
                    {
                        txt += $"{http.GetSite(url, get)}\n";
                    }
                    context.Channel.SendMessageAsync(txt);
                }
            } 
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** esse comando so pode ser usado em canais NSFW")
                    .Build());
            }
        }

        public void hentai(CommandContext context, object[] args)
        {
            nsfw(context, 1, "https://nekobot.xyz/api/image?type=hentai", "message");
        }

        public void hentaibomb(CommandContext context, object[] args)
        {
            nsfw(context, 5, "https://nekobot.xyz/api/image?type=hentai", "message");
        }
    }
}
