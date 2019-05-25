using Bot.DAO;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using System;
using Weeb.net;
using Weeb.net.Data;
using TokenType = Weeb.net.TokenType;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Weeb : Ajuda
    {
        private void weeb(CommandContext context, object[] args, string tipo, string msg, bool auto = true) //separa o object carai
        {
            ApiConfig ApiConfig = new ApiConfig(1);
            ApiConfigDAO ApiDao = new ApiConfigDAO();
            ApiConfig = ApiDao.Carregar(ApiConfig);

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(ApiConfig.weebToken, TokenType.Wolke).GetAwaiter().GetResult();
            RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False).GetAwaiter().GetResult();

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);
            embed.WithImageUrl(img.Url);

            if (auto)
            {
                if (!context.IsPrivate)
                {
                    string[] comando = (string[])args[1];
                    string cmd = string.Join(" ", comando, 1, (comando.Length - 1));

                    UserExtensions userExtensions = new UserExtensions();
                    Tuple<IUser, string> getUser = userExtensions.GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), cmd);

                    string user = "";
                    string author = userExtensions.GetNickname(context.User, !context.IsPrivate);

                    if (getUser.Item1 == null || getUser.Item1 == context.User)
                    {
                        user = "ele(a) mesmo";
                    }
                    else
                    {
                        user = userExtensions.GetNickname(getUser.Item1, !context.IsPrivate);
                    }

                    embed.WithTitle($"{author} {msg} {user}");
                }
                else
                {
                    embed.WithDescription("Esse comando só pode ser usado em servidores");
                    embed.WithColor(Color.Red);
                    embed.WithImageUrl(null);
                }
            }
            else
            {
                embed.WithTitle(msg);
            }

            context.Channel.SendMessageAsync(embed: embed.Build());
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
            weeb(context, args, "cry", "esta chorando com");
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
    }
}
