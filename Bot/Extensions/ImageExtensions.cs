using Discord;
using Discord.Commands;
using System;
using System.Threading;

namespace Bot.Extensions
{
    public class ImageExtensions
    {
        public void getImg(CommandContext contexto, string txt = "", Tuple<string, string> img = null, Tuple<string, string>[] imgs = null, bool nsfw = false, int quantidade = 1)
        {
            new Thread(() =>
            {
                if (imgs == null)
                {
                    Tuple<string, string>[] tuple =
                    {
                        img
                    };

                    imgs = tuple;
                }

                Random rand = new Random();
                int i = rand.Next(imgs.Length);

                HttpExtensions http = new HttpExtensions();

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);
                embed.WithImageUrl(http.GetSite(imgs[i].Item1, imgs[i].Item2));
                embed.WithTitle(txt);

                if (!nsfw)
                {
                    contexto.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    ITextChannel canal = contexto.Channel as ITextChannel;
                    if (contexto.IsPrivate || canal.IsNsfw)
                    {
                        if (quantidade <= 1)
                        {
                            contexto.Channel.SendMessageAsync(embed: embed.Build());
                        }
                        else
                        {
                            for (int x = 0; x < quantidade; x++)
                            {
                                int y = rand.Next(imgs.Length);
                                embed.WithImageUrl(http.GetSite(imgs[i].Item1, imgs[i].Item2));
                                contexto.Channel.SendMessageAsync(embed: embed.Build());
                            }
                        }
                    }
                    else
                    {
                        embed.WithImageUrl(null);
                        embed.WithColor(Color.Red);
                        embed.WithDescription(StringCatch.GetString("imgNsfw", "**{0}** esse comando só pode ser usado em canais NSFW", contexto.User.ToString()));
                        contexto.Channel.SendMessageAsync(embed: embed.Build());
                    }
                }
            }).Start();
        }
    }
}
