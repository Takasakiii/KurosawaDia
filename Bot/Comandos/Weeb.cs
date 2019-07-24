using Bot.Extensions;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
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
            ApiConfig apiConfig = new ApiConfigDAO().Carregar();

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(apiConfig.WeebToken, TokenType.Wolke).GetAwaiter().GetResult();
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
                        user = StringCatch.GetString("weebSelf", "ele(a) mesmo");
                    }
                    else
                    {
                        user = userExtensions.GetNickname(getUser.Item1, !context.IsPrivate);
                    }

                    embed.WithTitle($"{author} {msg} {user}");
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("weebDm", "Desculpe, mas so posso execultar esse comando em um servidor 😔"));
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
            weeb(context, args, "hug", StringCatch.GetString("hugTxt", "esta abraçando"));
        }

        public void kiss(CommandContext context, object[] args)
        {
            weeb(context, args, "kiss", StringCatch.GetString("kissTxt", "esta beijando"));
        }

        public void slap(CommandContext context, object[] args)
        {
            weeb(context, args, "slap", StringCatch.GetString("slapTxt", "esta dando um tapa no"));
        }

        public void punch(CommandContext context, object[] args)
        {
            weeb(context, args, "punch", StringCatch.GetString("punchTxt", "esta dando um soco no"));
        }

        public void lick(CommandContext context, object[] args)
        {
            weeb(context, args, "lick", StringCatch.GetString("lickTxt", "esta lambendo o"));
        }

        public void cry(CommandContext context, object[] args)
        {
            weeb(context, args, "cry", StringCatch.GetString("cryTxt", "esta chorando com"));
        }

        public void megumin(CommandContext context, object[] args)
        {
            weeb(context, args, "megumin", StringCatch.GetString("meguminTxt", "Megumin ❤"), false);
        }

        public void rem(CommandContext context, object[] args)
        {
            weeb(context, args, "rem", StringCatch.GetString("remTxt", "rem ❤"), false);
        }

        public void pat(CommandContext context, object[] args)
        {
            weeb(context, args, "pat", StringCatch.GetString("patTxt", "esta fazendo carinho no"));
        }

        public void dance(CommandContext context, object[] args)
        {
            weeb(context, args, "dance", StringCatch.GetString("danceTx", "{0} começou a dançar", new UserExtensions().GetNickname(context.User, !context.IsPrivate)), false);
        }
    }
}
