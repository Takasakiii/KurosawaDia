using Bot.Extensions;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Weeb.net;
using Weeb.net.Data;
using TokenType = Weeb.net.TokenType;

namespace Bot.Comandos
{
    public class Weeb : Moderacao
    {
        private void weeb(CommandContext context, object[] args, string tipo, string msg, bool auto = true)
        {

            WeebClient weebClient = new WeebClient();

            ApiConfig config = (ApiConfig)args[2];
            weebClient.Authenticate(config.weebToken, TokenType.Wolke).GetAwaiter().GetResult();

            RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False).GetAwaiter().GetResult();
            string[] nome = new string[2];

            System.Tuple<bool, IUser> getUser = new Extensions.UserExtensions().GetUserAsync(context, args);
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
            else
            {
                nome[0] = "ele(a) mesmo";
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

            string txt = "";
            if (auto)
            {
                txt = $"{nome[1]} {msg} {nome[0]}";
            }
            else
            {
                txt = msg.Replace("%author%", nome[1]);
            }

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle(txt)
                    .WithImageUrl(img.Url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void hug(CommandContext context, object[] args)
        {
            weeb(context, args, "hug", "esta abraçando");
        }

        public void kiss(CommandContext context, object[] args)
        {
            weeb(context, args, "kiss", "esta beijando");
        }

        public void slap(CommandContext context, object[] args)
        {
            weeb(context, args, "slap", "esta dando um tapa no");
        }

        public void punch(CommandContext context, object[] args)
        {
            weeb(context, args, "punch", "esta dando um soco no");
        }

        public void lick(CommandContext context, object[] args)
        {
            weeb(context, args, "lick", "esta lambendo o");
        }

        public void cry(CommandContext context, object[] args)
        {
            weeb(context, args, "cry", "%author% esta chorando", false);
        }

        public void megumin(CommandContext context, object[] args)
        {
            weeb(context, args, "megumin", "Megumin ❤", false);
        }

        public void rem(CommandContext context, object[] args)
        {
            weeb(context, args, "rem", "rem ❤", false);
        }

        public void pat(CommandContext context, object[] args)
        {
            weeb(context, args, "pat", "fazendo carinho no");

        }

        //decepção = decepção + 3000%;
    }
}
