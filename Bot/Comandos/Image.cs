using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Comandos
{
    public class Image : Nsfw
    {
        HttpExtensions http = new HttpExtensions();
        public void neko(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/neko", "url"); //this is a constructor

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Um pouco de meninas gato (ou gatos com skin) sempre faz bem")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void cat(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/meow", "url"); //construtor com repetição

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Meow")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void dog(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://random.dog/woof.json", "url"); //¯\_(ツ)_/¯

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Woof")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void img(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/avatar", "url"); //¯\_(ツ)_/¯

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Uma simples imagem para usar onde quiser (ou não kerek)")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void fuck(CommandContext context, object[] args)
        {
            string[] nome = new string[2];

            if (context.Message.MentionedUserIds.Count != 0)
            {
                SocketGuildUser user = new User().GetUserAsync(context).GetAwaiter().GetResult() as SocketGuildUser;

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
            };

            string img = new ArrayExtensions().GetRandom(imgs);

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle($"{nome[1]} esta fudendo com {nome[0]}")
                    .WithImageUrl(img)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
