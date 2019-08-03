using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Configuracoes : CustomReactions
    {
        public void setprefix(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    IUserMessage message = context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("setprefixCtz", "**{0}** você quer mudar o prefixo?", context.User))
                            .WithFooter(StringCatch.GetString("setprefixIgnorar", "se não apenas ignore essa mensagem"))
                            .WithColor(Color.DarkPurple)
                        .Build()).GetAwaiter().GetResult();

                    Emoji emoji = new Emoji("✅");
                    message.AddReactionAsync(emoji);

                    ReactionControler reaction = new ReactionControler();
                    reaction.GetReaction(message, emoji, context.User, new ReturnMethod((CommandContext contexto, object[] argumentos) =>
                    {
                        Servidores servidor = new Servidores(context.Guild.Id);
                        servidor.SetPrefix(msg.ToCharArray());

                        servidor = new ServidoresDAO().SetServidorPrefix(servidor);

                        message.DeleteAsync();
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setperfixAlterado", "**{0}** o prefixo do servidor foi alterado de: `{1}` para: `{2}`", context.User.ToString(), (string)args[0], new string(servidor.prefix)))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }, context, args));
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("setprefixFalarPrefixo", "**{0}** você precisa me falar um prefixo", context.User.ToString()))
                            .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoSetprefix", "`{0}setprefix <prefixo>`", (string)args[0]))
                            .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploCmd", "`{0}setprefix !`", (string)args[0]))
                            .WithColor(Color.Red)
                        .Build());
                }

            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("setprefixDm", "Esse comando so pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void xprole(CommandContext context, object[] args)
        {
            context.Channel.SendMessageAsync("Digite algo");
            SubCommandControler cmd = new SubCommandControler();
            IMessage msg = cmd.GetCommand(context.Channel, context.User);
            context.Channel.SendMessageAsync(msg.Content);
        }
    }
}
