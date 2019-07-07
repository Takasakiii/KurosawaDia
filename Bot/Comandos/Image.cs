using Bot.Constantes;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading;

namespace Bot.Comandos
{
    public class Image : Moderacao
    {
        public Links links = new Links();
        public void getImg(CommandContext context, string txt = "", Tuple<string, string> img = null, Tuple<string, string>[] imgs = null, bool nsfw = false, int quantidade = 1)
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
                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    ITextChannel canal = context.Channel as ITextChannel;
                    if (context.IsPrivate || canal.IsNsfw)
                    {
                        if (quantidade <= 1)
                        {
                            context.Channel.SendMessageAsync(embed: embed.Build());
                        }
                        else
                        {
                            for (int x = 0; x < quantidade; x++)
                            {
                                int y = rand.Next(imgs.Length);
                                embed.WithImageUrl(http.GetSite(imgs[i].Item1, imgs[i].Item2));
                                context.Channel.SendMessageAsync(embed: embed.Build());
                            }
                        }
                    }
                    else
                    {
                        embed.WithImageUrl(null);
                        embed.WithColor(Color.Red);
                        embed.WithDescription($"**{context.User}** esse comando só pode ser usado em canais NSFW");
                        context.Channel.SendMessageAsync(embed: embed.Build());
                    }
                }
            }).Start();
        }

        public void neko(CommandContext context, object[] args)
        {
            getImg(context, "Um pouco de meninas gato (ou gatos com skin) sempre faz bem", links.neko);
        }

        public void cat(CommandContext context, object[] args)
        {
            getImg(context, "Meow", links.cat);
        }

        public void dog(CommandContext context, object[] args)
        {
            getImg(context, "Meow", links.dog);
        }

        public void img(CommandContext context, object[] args)
        {
            getImg(context, "Uma simples imagem pra usar onde quiser", links.img);
        }
    }
}
