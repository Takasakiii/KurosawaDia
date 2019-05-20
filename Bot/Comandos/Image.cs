using Bot.Extensions;
using Bot.Modelos;
using Discord;
using Discord.Commands;
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
            if (!context.IsPrivate)
            {
                string[] nome = new string[2];

                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                Extensions.UserExtensions userExtensions = new Extensions.UserExtensions();
                Tuple<IUser, string> getUser = userExtensions.GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                userExtensions.GetNickname(getUser.Item1, !context.IsPrivate);
                userExtensions.GetNickname(context.User, !context.IsPrivate);

                string[] imgs = {
                "https://i.imgur.com/KYFJQLY.gif",
                "https://i.imgur.com/OXixXxm.gif",
                "https://i.imgur.com/LQT87mc.gif",
                "https://i.imgur.com/4LNI3Nh.gif",
                "https://i.imgur.com/pPz7p2s.gif"
            }; // constante not is var


                Random rand = new Random();
                int i = rand.Next(imgs.Length);

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"{nome[1]} esta fudendo com {nome[0]}")
                        .WithImageUrl(imgs[i])
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("Você so pode usar esse comando em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }
    }
}
