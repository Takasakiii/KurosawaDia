using Bot.Extensions;
using Bot.GenericTypes;
using ConfigurationControler.DAO;
using Discord;
using Discord.Commands;
using System;
using Weeb.net;
using Weeb.net.Data;
using TokenType = Weeb.net.TokenType;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Weeb : GenericModule
    {
        public Weeb(CommandContext contexto, object[] args) : base (contexto, args)
        {

        }

        private void weeb(string tipo, string msg, bool auto = true)
        {
            var apiConfig = new ApisConfigDAO().Carregar();

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(apiConfig.Item2[0].Token, TokenType.Wolke).GetAwaiter().GetResult();
            RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False).GetAwaiter().GetResult();

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);
            embed.WithImageUrl(img.Url);

            if (auto)
            {
                if (!contexto.IsPrivate)
                {
                    string[] comando = (string[])args[1];
                    string cmd = string.Join(" ", comando, 1, (comando.Length - 1));

                    UserExtensions userExtensions = new UserExtensions();
                    Tuple<IUser, string> getUser = userExtensions.GetUser(contexto.Guild.GetUsersAsync().GetAwaiter().GetResult(), cmd);

                    string user = "";
                    string author = userExtensions.GetNickname(contexto.User, !contexto.IsPrivate);

                    if (getUser.Item1 == null || getUser.Item1 == contexto.User)
                    {
                        user = StringCatch.GetString("weebSelf", "ele(a) mesmo");
                    }
                    else
                    {
                        user = userExtensions.GetNickname(getUser.Item1, !contexto.IsPrivate);
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

            contexto.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void hug()
        {
            weeb("hug", StringCatch.GetString("hugTxt", "esta abraçando"));
        }

        public void kiss()
        {
            weeb("kiss", StringCatch.GetString("kissTxt", "esta beijando"));
        }

        public void slap()
        {
            weeb("slap", StringCatch.GetString("slapTxt", "esta dando um tapa no"));
        }

        public void punch()
        {
            weeb("punch", StringCatch.GetString("punchTxt", "esta dando um soco no"));
        }

        public void lick()
        {
            weeb("lick", StringCatch.GetString("lickTxt", "esta lambendo o"));
        }

        public void cry()
        {
            weeb("cry", StringCatch.GetString("cryTxt", "esta chorando com"));
        }

        public void megumin()
        {
            weeb("megumin", StringCatch.GetString("meguminTxt", "Megumin ❤"), false);
        }

        public void rem()
        {
            weeb("rem", StringCatch.GetString("remTxt", "rem ❤"), false);
        }

        public void pat()
        {
            weeb("pat", StringCatch.GetString("patTxt", "esta fazendo carinho no"));
        }

        public void dance()
        {
            weeb("dance", StringCatch.GetString("danceTx", "{0} começou a dançar", new UserExtensions().GetNickname(contexto.User, !contexto.IsPrivate)), false);
        }
    }
}
