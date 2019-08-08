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
                if (comando.Length > 1)
                {
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                    string[] parms = msg.Split('|');
                    if (parms.Length >= 3 || parms[0] == "n")
                    {
                        bool ativado = false;
                        if (parms[0] == "s")
                        {
                            ativado = true;
                        }

                        if (ativado)
                        {
                            double rate = 0;
                            if (double.TryParse(parms[1], out rate) && rate >= 1)
                            {
                                ConfiguracoesServidor configs = new ConfiguracoesServidor(new Servidores(context.Guild.Id, context.Guild.Name), new PI(ativado, rate, parms[2]));
                                if (new ConfiguracoesServidorDAO().SalvarPIConfig(configs))
                                {
                                    embed.WithDescription(StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"));
                                    embed.WithColor(Color.DarkPurple);
                                }
                                else
                                {
                                    embed.WithDescription(StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"));
                                    embed.WithColor(Color.Red);
                                }
                            }
                            else
                            {
                                rate = 2;
                            }
                        }
                        else
                        {
                            ConfiguracoesServidor configs = new ConfiguracoesServidor(new Servidores(context.Guild.Id, context.Guild.Name), new PI(false));
                            if (new ConfiguracoesServidorDAO().SalvarPIConfig(configs))
                            {
                                embed.WithDescription(StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"));
                                embed.WithColor(Color.DarkPurple);
                            }
                            else
                            {
                                embed.WithDescription(StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"));
                                embed.WithColor(Color.Red);
                            }
                        }
                    }
                    else
                    {
                        embed.WithTitle(StringCatch.GetString("xproleErro", "Desculpe, mas o formato do comando está incorreta 😓"));
                        embed.AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoXprole", "`{0}xprole ativado <s/n> | rate | msg`", (string)args[0]));
                        embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("exemploXprole", "`{0}xprole s | 1.2 | parabens você subiu de nivel`", (string)args[0]));
                        embed.WithColor(Color.Red);
                    }
                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    embed.WithColor(Color.Purple);
                    embed.WithTitle(StringCatch.GetString("xproleSetTitle", "**Configuração dos Pontos de Interação**"));
                    embed.WithDescription(StringCatch.GetString("xproleSetDesc1", "Você deseja ligar os pontos de interação??(eles servem para medir a interação dos seus membros e setar cargos automaticamente)"));
                    embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSetF1Desc", "s - Sim / Ligar\nn - Não / Desligar"));
                    embed.AddField(StringCatch.GetString("xproleSetF2", "Dicas:"), StringCatch.GetString("xproleSetF2Desc", "Você pode desativar facilmente usando o `{0}xprole n`\nOutras ações tambem poderão ser realizadas rapidamente somente com um comando", (string)args[0]));
                    IMessage pergunta = context.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = sub.GetCommand(pergunta, context.User);
                    if(msgresposta != null)
                    {
                        bool ativado;
                        double rate = 2;
                        string msg = "";
                        if (msgresposta.Content == "s" || msgresposta.Content == "n")
                        {
                            
                            if(msgresposta.Content == "s")
                            {
                                ativado = true;
                                embed.WithDescription(StringCatch.GetString("xproleSetDesc2", "Qual é o multiplicador de Pontos de Interação que deseja usar (esse multiplicador determina como sera medido a interação dos membros) [recomendamos o multiplicador 2]"));
                                embed.Fields.Clear();
                                embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet2F1Desc", "Qualquer numero a partir de 1,0"));
                                pergunta = context.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                sub = new SubCommandControler();
                                msgresposta = sub.GetCommand(pergunta, context.User);
                                if (double.TryParse(msgresposta.Content, out rate))
                                {
                                    if (rate >= 1)
                                    {
                                        embed.WithDescription(StringCatch.GetString("xproleSetDesc3", "Digite a messagem que você quer que eu mostre quando alguem conseguir um Ponto de Interação, se você não deseja ter uma mensagem apenas digite `%desativar%`"));
                                        embed.Fields.Clear();
                                        embed.AddField(StringCatch.GetString("xptoleSetF1", "Opções Validas:"), StringCatch.GetString("xproleSet3F1Desc", "Qualquer tipo de texto, podendo usar até Embeds compativel com a Nadeko Bot e variaveis como %user% e %pontos%"));
                                        pergunta = context.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                                        sub = new SubCommandControler();
                                        msgresposta = sub.GetCommand(pergunta, context.User);
                                        msg = msgresposta.Content;
                                    }
                                    else
                                    {
                                        RotaFail(context);
                                    }
                                }
                                else
                                {
                                    RotaFail(context);
                                }
                            }
                            else
                            {
                                ativado = false;
                            }
                            PI pimodel = new PI(ativado, rate, (msg == "%desativar%") ? "" : msg);
                            if (new ConfiguracoesServidorDAO().SalvarPIConfig(new ConfiguracoesServidor(new Servidores(context.Guild.Id, context.Guild.Name), pimodel)))
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Green)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleOK", "Ok, farei tudo conforme o pedido 😃"))
                                    .Build());
                            }
                            else
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithColor(Color.Red)
                                    .WithTitle(StringCatch.GetString("xproleSetTitleFail", "Desculpe mas ouve um problema ao tentar salvar suas preferencias, se for urgente contate meus criadores que eles vão te dar todo o suporte 😔"))
                                    .Build());
                            }
                        }
                        else
                        {
                            RotaFail(context);
                        }
                        
                    }
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("xproleDm", "Esse comando só pode ser usado em servidores"));
                embed.WithColor(Color.Red);
                context.Channel.SendMessageAsync(embed: embed.Build());
            }
            
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
