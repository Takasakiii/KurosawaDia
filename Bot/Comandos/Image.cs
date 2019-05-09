using Bot.Extensions;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;

namespace Bot.Comandos
{
    public class Image : Nsfw
    {

        private void img(CommandContext context, string txt, Links link = null, Links[] links = null)
        {
            if (links == null) //kkkkkk conheço essa estrutura de algum lugar
            {
                links = new Links[1];
                links[0] = link;
            }

            Random rand = new Random();
            int i = rand.Next(links.Length);

            HttpExtensions http = new HttpExtensions();
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(txt)
                    .WithImageUrl(http.GetSite(links[i].url, links[i].tipo))
                    .WithColor(Color.DarkPurple)
                .Build());
        }
        public void neko(CommandContext context, object[] args)
        {
            img(context, "Um pouco de meninas gato (ou gatos com skin) sempre faz bem", new Links("https://nekos.life/api/v2/img/neko", "url")); // e n u m e r a t e
        }

        public void cat(CommandContext context, object[] args)
        {
            img(context, "Meow", new Links("https://nekos.life/api/v2/img/meow", "url")); // ¯\_(ツ)_/¯
        }

        public void dog(CommandContext context, object[] args)
        {
            img(context, "Meow", new Links("https://random.dog/woof.json", "url")); //¯\_(ツ)_ /¯
        }

        public void img(CommandContext context, object[] args)
        {
            img(context, "Uma simples imagem pra usar onde quiser", new Links("https://nekos.life/api/v2/img/avatar", "url")); //¯\_(ツ)_/¯
        }

        public void fuck(CommandContext context, object[] args)
        {
            string[] nome = new string[2];

            System.Tuple<bool, IUser> getUser = new Extensions.UserExtensions().GetUserAsync(context, args); //using system 
            if (getUser.Item1)
            {
                SocketGuildUser user = getUser.Item2 as SocketGuildUser;

                if (user.Nickname != null)
                {
                    nome[0] = user.Nickname;
                }
                else
                {
                    nome[0] = user.Username;
                }
            }

            SocketGuildUser userGuild = context.User as SocketGuildUser;

            if (userGuild.Nickname != null)
            {
                nome[1] = userGuild.Nickname;
            }
            else
            {
                nome[1] = userGuild.Username;
            }

            string[] imgs = {
                "https://i.imgur.com/KYFJQLY.gif",
                "https://i.imgur.com/OXixXxm.gif",
                "https://i.imgur.com/LQT87mc.gif",
                "https://i.imgur.com/4LNI3Nh.gif",
                "https://i.imgur.com/pPz7p2s.gif"
            }; // constante not is var

            string img = new ArrayExtensions().GetRandom(imgs);

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle($"{nome[1]} esta fudendo com {nome[0]}")
                    .WithImageUrl(img)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
