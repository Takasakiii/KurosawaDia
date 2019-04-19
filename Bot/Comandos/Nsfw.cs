using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Weeb
    {
        private void nsfw(CommandContext context, string url, string get = "message", int quantidade = 1)
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
            nsfw(context, "https://nekobot.xyz/api/image?type=hentai");
        }

        public void hentaibomb(CommandContext context, object[] args)
        {

            Links[] links = {
                new Links("https://nekobot.xyz/api/image?type=hentai", "message"),
                new Links("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
                new Links("https://nekos.life/api/v2/img/lewdk", "url"),
            };

            Random rand = new Random();
            int i = rand.Next(links.Length);

            nsfw(context,links[i].url, links[i].tipo, 5);
        }

        public void hneko(CommandContext context, object[] args)
        {
            string[] links = { "https://nekos.life/api/v2/img/nsfw_neko_gif", "https://nekos.life/api/v2/img/lewdk" };
            nsfw(context, new ArrayExtensions().GetRandom(links), "url");
        }
    }


    class Links
    {
        public string url {private set; get; }
        public string tipo { private set; get; }

        public Links(string url, string tipo)
        {
            this.url = url;
            this.tipo = tipo;
        }
    }
}


