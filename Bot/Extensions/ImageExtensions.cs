using Discord;
using Discord.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class ImageExtensions
    {
        public async Task getImg(CommandContext contexto, string txt = "", Tuple<string, string> img = null, Tuple<string, string>[] imgs = null, bool nsfw = false, int quantidade = 1)
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
            embed.WithImageUrl(await http.GetSite(imgs[i].Item1, imgs[i].Item2));
            embed.WithTitle(txt);

            if (!nsfw)
            {
                await contexto.Channel.SendMessageAsync(embed: embed.Build());
            }
            else
            {
                ITextChannel canal = contexto.Channel as ITextChannel;
                if (contexto.IsPrivate || canal.IsNsfw)
                {
                    if (quantidade <= 1)
                    {
                        await contexto.Channel.SendMessageAsync(embed: embed.Build());
                    }
                    else
                    {
                        for (int x = 0; x < quantidade; x++)
                        {
                            int y = rand.Next(imgs.Length);
                            embed.WithImageUrl(await http.GetSite(imgs[i].Item1, imgs[i].Item2));
                            await contexto.Channel.SendMessageAsync(embed: embed.Build());
                        }
                    }
                }
                else
                {
                    embed.WithImageUrl(null);
                    embed.WithColor(Color.Red);
                    embed.WithDescription(await StringCatch.GetString("imgNsfw", "**{0}** esse comando só pode ser usado em canais NSFW", contexto.User.ToString()));
                    await contexto.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
        }
    }
}
