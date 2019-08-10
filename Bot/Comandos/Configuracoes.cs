using Bot.Extensions;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Comandos
{
    public class Configuracoes : CustomReactions
    {
        public void setprefix(CommandContext context, object[] args)
        {
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                if (!cmdContext.IsPrivate)
                {
                    string[] comando = (string[])cmdArgs[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    if (msg != "")
                    {
                        IUserMessage message = cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setprefixCtz", "**{0}** você quer mudar o prefixo?", cmdContext.User))
                                .WithFooter(StringCatch.GetString("setprefixIgnorar", "se não apenas ignore essa mensagem"))
                                .WithColor(Color.DarkPurple)
                            .Build()).GetAwaiter().GetResult();

                        Emoji emoji = new Emoji("✅");
                        message.AddReactionAsync(emoji);

                        ReactionControler reaction = new ReactionControler();
                        reaction.GetReaction(message, emoji, cmdContext.User, new ReturnMethod((CommandContext contexto, object[] argumentos) =>
                        {
                            Servidores servidor = new Servidores(cmdContext.Guild.Id, msg.ToCharArray());

                            new ServidoresDAO().SetServidorPrefix(ref servidor);

                            message.DeleteAsync();
                            cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("setperfixAlterado", "**{0}** o prefixo do servidor foi alterado de: `{1}` para: `{2}`", cmdContext.User.ToString(), (string)cmdArgs[0], new string(servidor.Prefix)))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }, cmdContext, cmdArgs));
                    }
                    else
                    {
                        cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setprefixFalarPrefixo", "**{0}** você precisa me falar um prefixo", cmdContext.User.ToString()))
                                .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoSetprefix", "`{0}setprefix <prefixo>`", (string)cmdArgs[0]))
                                .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploCmd", "`{0}setprefix !`", (string)cmdArgs[0]))
                                .WithColor(Color.Red)
                            .Build());
                    }

                }
                else
                {
                    cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("setprefixDm", "Esse comando so pode ser usado em servidores"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }, context, args).EsperarOkDb();
        }

        public void xprole(CommandContext context, object[] args)
        {
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);

                if (!cmdContext.IsPrivate)
                {
                    embed.WithColor(Color.Purple);
                    embed.WithTitle(StringCatch.GetString("xproleSetTitle", "**Configuração dos Pontos de Interação**"));
                    embed.WithDescription(StringCatch.GetString("xproleSetDesc1", "Você deseja ligar os pontos de interação??(eles servem para medir a interação dos seus membros e setar cargos automaticamente)"));
                    embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSetF1Desc", "s - Sim / Ligar\nn - Não / Desligar"));
                    IMessage pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                    if (msgresposta != null)
                    {
                        bool ativado;
                        double rate = 2;
                        string msg = "";
                        if (msgresposta.Content == "s" || msgresposta.Content == "n")
                        {

                            if (msgresposta.Content == "s")
                            {
                                ativado = true;
                                embed.WithDescription(StringCatch.GetString("xproleSetDesc2", "Qual é o multiplicador de Pontos de Interação que deseja usar (esse multiplicador determina como sera medido a interação dos membros) [recomendamos o multiplicador 2]"));
                                embed.Fields.Clear();
                                embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet2F1Desc", "Qualquer numero a partir de 1,0"));
                                pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                sub = new SubCommandControler();
                                msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                                if (double.TryParse(msgresposta.Content, out rate))
                                {
                                    if (rate >= 1)
                                    {
                                        embed.WithDescription(StringCatch.GetString("xproleSetDesc3", "Digite a messagem que você quer que eu mostre quando alguem conseguir um Ponto de Interação, se você não deseja ter uma mensagem apenas digite `%desativar%`"));
                                        embed.Fields.Clear();
                                        embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet3F1Desc", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user% e %pontos%"));
                                        pergunta = cmdContext.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                        sub = new SubCommandControler();
                                        msgresposta = sub.GetCommand(pergunta, cmdContext.User);
                                        msg = msgresposta.Content;
                                    }
                                    else
                                    {
                                        RotaFail(cmdContext);
                                    }
                                }
                                else
                                {
                                    RotaFail(cmdContext);
                                }
                            }
                            else
                            {
                                ativado = false;
                            }
                            PI pimodel = new PI(ativado, rate, (msg == "%desativar%") ? "" : msg);
                            if (new ConfiguracoesServidorDAO().SalvarPIConfig(new ConfiguracoesServidor(new Servidores(cmdContext.Guild.Id), pimodel)))
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"))
                                    .Build());
                            }
                            else
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Red)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"))
                                    .Build());
                            }
                        }
                        else
                        {
                            RotaFail(cmdContext);
                        }

                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("xproleDm", "Esse comando só pode ser usado em servidores"));
                    embed.WithColor(Color.Red);
                    cmdContext.Channel.SendMessageAsync(embed: embed.Build());
                }
            }, context, args).EsperarOkDb();
        }

        private void RotaFail(CommandContext contexto)
        {
            contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(StringCatch.GetString("rotafailtitle", "Desculpe, mas você terá que me falar um valor dentro do **Opções Validas**, se não eu não poderei te ajudar 😔"))
                .Build());
        }

        public void setwelcome(CommandContext context, object[] args)
        {

        }
    }
}
