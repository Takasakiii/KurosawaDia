using Bot.Extensions;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using System;

namespace Bot.Comandos
{
    public class Nsfw : Weeb
    {
        private void nsfw(CommandContext context, Links[] links = null, Links link = null, int quantidade = 1)
        {
            ITextChannel canal = context.Channel as ITextChannel;
            if (context.IsPrivate || canal.IsNsfw) 
            {
                if (links == null)// 2019 - Kurosawa Dia - Todos os Direitos Reservador - Takasaki
                {
                    links = new Links[1];
                    links[0] = link;
                }

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

                    context.Channel.SendMessageAsync(txt); // padronização ta top
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
            Links[] links = {
                new Links("https://nekobot.xyz/api/image?type=hentai", "message"),
                new Links("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
                new Links("https://nekos.life/api/v2/img/lewdk", "url"),
            }; // constantes enumeradaaaaassss (se odeia enumerate pelo menos deixa em constante fdp) atacar

            nsfw(context, links);
        }

        public void hentaibomb(CommandContext context, object[] args)
        {

            Links[] links = {
                new Links("https://nekobot.xyz/api/image?type=hentai", "message"),
                new Links("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
                new Links("https://nekos.life/api/v2/img/lewdk", "url"),
            }; //¯\_(ツ)_/¯

            nsfw(context, links, quantidade: 5);
        }

        public void hneko(CommandContext context, object[] args)
        {
            Links[] links = {
                new Links("https://nekos.life/api/v2/img/lewdki", "url"),
                new Links("https://nekos.life/api/v2/img/nsfw_neko_gif", "url"),
            };//¯\_(ツ)_ /¯

            nsfw(context, links);
        }

        public void anal(CommandContext context, object[] args)
        {
            nsfw(context, link: new Links("https://nekobot.xyz/api/image?type=anal", "message")); //enumirati

        }
    }
}


