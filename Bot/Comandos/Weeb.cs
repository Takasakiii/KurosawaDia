using Bot.Extensions;
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
    public class Weeb : Moderacao
    {
        private void weeb(CommandContext context, object[] args, string tipo, string msg, bool auto = true) //separa o object carai
        {
            string[] comando = (string[])args[1];
            string cmd = string.Join(" ", comando, 1, (comando.Length - 1));

            UserExtensions userExtensions = new UserExtensions();

            Tuple<IUser, string> getUser = userExtensions.GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), cmd);

            if (getUser.Item1 != null)
            {
                WeebClient weebClient = new WeebClient();
                ApiConfig config = (ApiConfig)args[2];
                weebClient.Authenticate(config.weebToken, TokenType.Wolke).GetAwaiter().GetResult();

                RandomData img = weebClient.GetRandomAsync(tipo, new string[] { }, FileType.Gif, false, NsfwSearch.False).GetAwaiter().GetResult();
                string[] nome = new string[2];

                nome[0] = userExtensions.GetNickname(context.User, !context.IsPrivate);
                nome[1] = userExtensions.GetNickname(getUser.Item1, !context.IsPrivate);

                string txt = "";
                if (auto)
                {
                    txt = $"{nome[0]} {msg} {nome[1]}";
                }
                else
                {
                    txt = msg;
                }

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle(txt)
                        .WithImageUrl(img.Url)
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithDescription($"**{context.User}** não encontrei essa pessoa")
                    .AddField("Uso do Comando: ", $"`{(string)args[0]}{comando[0]} @pessoa`")
                    .AddField("Exemplo: ", $"`{(string)args[0]}{comando[0]} @Tamires Lima#4256`")
                    .WithColor(Color.Red)
                 .Build());
            }
        } //refaz

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
