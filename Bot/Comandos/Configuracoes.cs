using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using static Bot.DataBase.MainDB.Modelos.ConfiguracoesServidor;

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
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            if (!context.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                string[] parms = msg.Split('|');

                if(parms.Length > 3 || parms[0] == "n" || parms[0] == "não")
                {
                    bool ativado = false;
                    if (parms[0] == "s" || parms[0] == "sim")
                    {
                        ativado = true;
                    }

                    if (ativado)
                    {
                        double rate = 0;
                        if (double.TryParse(parms[1], out rate))
                        {
                            ConfiguracoesServidor configs = new ConfiguracoesServidor(new Servidores(context.Guild.Id, context.Guild.Name), new PI(ativado, rate, parms[2]));
                            if(new ConfiguracoesServidorDAO().SalvarPIConfig(configs))
                            {
                                embed.WithDescription(StringCatch.GetString("xproleSetado", "**{0}** setado yay", context.User.ToString()));
                                embed.WithColor(Color.DarkPurple);
                            }
                            else
                            {
                                embed.WithDescription(StringCatch.GetString("xproleRateErro", "**{0}** deu merda na hr de seta", context.User.ToString()));
                                embed.WithColor(Color.Red);
                            }
                        }
                        else
                        {
                            embed.WithDescription(StringCatch.GetString("xproleRateErro", "**{0}** tem um erro na paryte do rate yay", context.User.ToString()));
                            embed.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        ConfiguracoesServidor configs = new ConfiguracoesServidor(new Servidores(context.Guild.Id, context.Guild.Name), new PI(false));
                        if (new ConfiguracoesServidorDAO().SalvarPIConfig(configs))
                        {
                            embed.WithDescription(StringCatch.GetString("xproleSetado", "**{0}** setado yay", context.User.ToString()));
                            embed.WithColor(Color.DarkPurple);
                        }
                        else
                        {
                            embed.WithDescription(StringCatch.GetString("xproleRateErro", "**{0}** deu merda na hr de seta", context.User.ToString()));
                            embed.WithColor(Color.Red);
                        }
                    }
                }
                else
                {
                    embed.WithTitle(StringCatch.GetString("xproleErro", "Você precisa me falar qm caralhas eu tenho q colocar na bosta das config dessa porra de server"));
                    embed.AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoXprole", "`{0}xprole ativado <s/n> | rate | msg`", (string)args[0]));
                    embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("exemploXprole", "`{0}xprole s | 1.2 | parabens você subiu de nivel`", (string)args[0]));
                    embed.WithColor(Color.Red);
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("xproleDm", "Esse comando só pode ser usado em servidores"));
                embed.WithColor(Color.Red);
            }
            context.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void setwelcome(CommandContext context, object[] args)
        {

        }
    }
}
