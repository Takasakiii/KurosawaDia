using Bot.Constructor;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using Weeb.net;
using Weeb.net.Data;

namespace Bot.Comandos
{
    public class Weeb : Moderacao
    {
        private void weeb(CommandContext context, string tipo, string msg, bool auto = true)
        {
             WeebClient weebClient = new WeebGen().weebClient;
             RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False).GetAwaiter().GetResult();
             string[] nome = new string[2];

            if(context.Message.MentionedUserIds.Count != 0)
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

            if(userGuild.Nickname != null)
            {
                nome[1] = userGuild.Nickname;
            } else
            {
                nome[1] = userGuild.Username;
            }

            string txt = "";
            if(auto)
            {
                txt = $"{nome[1]} {msg} {nome[0]}";
            } else
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
            weeb(context, "hug", "esta abraçando");
        }

        public void kiss(CommandContext context, object[] args)
        {
            weeb(context, "kiss", "esta beijando");
        }

        public void slap(CommandContext context, object[] args)
        {
            weeb(context, "slap", "esta dando um tapa no");
        }

        public void punch(CommandContext context, object[] args)
        {
            weeb(context, "punch", "esta dando um soco no");
        }

        public void lick(CommandContext context, object[] args)
        {
            weeb(context, "lick", "esta lambendo o");
        }

        public void cry(CommandContext context, object[] args)
        {
            weeb(context, "cry", "%author% esta chorando", false);
        }

        public void megumin(CommandContext context, object[] args)
        {
            weeb(context, "megumin", "Megumin ❤", false);
        }

        public void rem(CommandContext context, object[] args)
        {
            weeb(context, "rem", "rem ❤", false);
        }

        public void pat(CommandContext context, object[] args)
        {
            weeb(context, "pat", "fazendo carinho no");

        }

        //decepção = decepção + 3000%;
    }
}
