using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Weeb
    {
        private void nsfw(CommandContext context, Link[] links, int quantidade = 1)
        {
            ITextChannel canal = context.Channel as ITextChannel;
            if (context.IsPrivate || canal.IsNsfw)
            {
                HttpExtensions http = new HttpExtensions();

                if (quantidade <= 1)
                {
                    Random rand = new Random();
                    int i = rand.Next(links.Length);

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithImageUrl(http.GetSite(links[i].url, links[i].tipo))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    string txt = "";
                    for (int i = 0; i < quantidade; i++)
                    {
                        Random rand = new Random();
                        int x = rand.Next(links.Length);

                        txt += $"{http.GetSite(links[x].url, links[x].tipo)}\n";
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
            Link[] links = {
                new Link("https://nekobot.xyz/api/image?type=hentai", "message"),
                new Link("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
                new Link("https://nekos.life/api/v2/img/lewdk", "url"),
            };

            nsfw(context, links);
        }

        public void hentaibomb(CommandContext context, object[] args)
        {

            Link[] links = {
                new Link("https://nekobot.xyz/api/image?type=hentai", "message"),
                new Link("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
                new Link("https://nekos.life/api/v2/img/lewdk", "url"),
            };

            nsfw(context, links, 5);
        }

        public void hneko(CommandContext context, object[] args)
        {
            Link[] links = {
                new Link("https://nekos.life/api/v2/img/lewdki", "url"),
                new Link("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
            };

            nsfw(context, links);
        }

        public void anal(CommandContext context, object[] args)
        {
            Link[] links = {
                new Link( "https://nekobot.xyz/api/image?type=anal", "message"),
            };

            nsfw(context, links);

        }
    }


    class Link
    {
        public string url { private set; get; }
        public string tipo { private set; get; }

        public Link(string url, string tipo)
        {
            this.url = url;
            this.tipo = tipo;
        }
    }
}


